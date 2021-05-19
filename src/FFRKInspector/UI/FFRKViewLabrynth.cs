using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FFRKInspector.GameData;
using FFRKInspector.Proxy;

namespace FFRKInspector.UI
{
    public partial class FFRKViewLabrynth : UserControl
    {
        private Dictionary<int, string> paintingImages = new Dictionary<int, string>();
        public FFRKViewLabrynth()
        {
            InitializeComponent();
            paintingImages[3] = "/dff/static/lang/image/labyrinth_dungeon/painting/painting_img/treasure_house_painting.png";
            paintingImages[5] = "/dff/static/lang/image/labyrinth_dungeon/painting/painting_img/advance_painting.png";
            paintingImages[4] = "/dff/static/lang/image/labyrinth_dungeon/painting/painting_img/explore_painting.png";
            paintingImages[6] = "/dff/static/lang/image/labyrinth_dungeon/painting/painting_img/transfer_painting.png";
            paintingImages[7] = "/dff/static/lang/image/labyrinth_dungeon/painting/painting_img/healing_painting.png";

            LoadPaintingImage("/dff/static/img/category/labyrinth_dungeon/painting/treasure_box_01.png", pbTreasure1);
            LoadPaintingImage("/dff/static/img/category/labyrinth_dungeon/painting/treasure_box_02.png", pbTreasure2);
            LoadPaintingImage("/dff/static/img/category/labyrinth_dungeon/painting/treasure_box_03.png", pbTreasure3);
        }


        private void LoadPaintingImage(string url, PictureBox pictureBox)
        {
            try
            {
                using (var response = WebRequest.Create(string.Format("https://dff.sp.mbga.jp{0}", url)).GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        pictureBox.Image = Image.FromStream(responseStream);
                    }
                }
            }
            catch (Exception)
            {
                pictureBox.Image = null;
            }

        }

        private string GetImageUrlFromPainting(LabrynthPainting painting)
        {
            if (painting.LabrynthDungeon != null && painting.LabrynthDungeon.LabrynthCapture != null)
                return painting.LabrynthDungeon.LabrynthCapture[0].ImagePath;

            if (paintingImages.ContainsKey(painting.Type))
                return paintingImages[painting.Type];

            return "/dff/static/img/category/labyrinth_dungeon/painting/treasure_box_01.png";
        }

        private void FFRKProxy_LabrynthSessionDataUpdated(LabrynthSessionData labrynthSessionData) => this.BeginInvoke((Action)(() =>
        {
            LoadTreasure(labrynthSessionData);
            var paintings = labrynthSessionData.LabrynthSession.LabrynthPaintings;
            var pbPaintings = new List<PictureBox>
            {
                pbPainting1, pbPainting2, pbPainting3, pbPainting4, pbPainting5, pbPainting6, pbPainting7, pbPainting8,
                pbPainting9
            };

            foreach (var pbPainting in pbPaintings)
            {
                pbPainting.Image = null;
                pbPainting.BorderStyle = BorderStyle.None;
            }

            for (int i = 0; i < paintings.Count; i++)
            {
                LoadPaintingImage(GetImageUrlFromPainting(paintings[i]), pbPaintings[i]);
            }

        }));

        private void LoadTreasure(LabrynthSessionData labrynthSessionData)
        {
            gbTreasure.Visible = false;

            if (labrynthSessionData.LabrynthSession.ChestIds != null)
            {
                gbTreasure.Visible = true;
                SetChestContents(labrynthSessionData.LabrynthSession.ChestIds[0], lblTreasure1);
                SetChestContents(labrynthSessionData.LabrynthSession.ChestIds[1], lblTreasure2);
                SetChestContents(labrynthSessionData.LabrynthSession.ChestIds[2], lblTreasure3);
            }
            else
            {
                gbTreasure.Visible = false;
            }
        }

        private void SetChestContents(int chestContentsId, Label label)
        {
            label.Text = (chestContentsId >= 500000 && chestContentsId < 600000) ? "Equipment" : "";
        }

        private void FFRKViewLabrynth_Load(object sender, EventArgs e)
        {
            if (FFRKProxy.Instance != null)
            {
                FFRKProxy.Instance.OnLabrynthSessionUpdated += FFRKProxy_LabrynthSessionDataUpdated;

            }
        }

    }
}
