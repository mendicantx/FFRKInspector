using System;
using FFRKInspector.GameData;
using Fiddler;

namespace FFRKInspector.Functionality
{
    public class ItemNameGetter
    {

        public string GetItemName(uint ItemId, uint num, string defaultName)
        {
            var str1 = "";
            if (ItemId > 40000000U && ItemId <= 40000065U)
            {
                var str2 = "";
                switch (ItemId % 5U)
                {
                    case 0:
                        str2 += "Major ";
                        break;
                    case 1:
                        str2 += "Minor ";
                        break;
                    case 2:
                        str2 += "Lesser ";
                        break;
                    case 4:
                        str2 += "Greater ";
                        break;
                }

                str1 = str2 + Enum.GetName(typeof(SchemaConstants.ItemID), (uint)((int)((ItemId - 1U) / 5U) * 5 + 5));
            }
            else if (ItemId >= 40000066U && ItemId <= 40000078U)
            {
                str1 = Enum.GetName(typeof(SchemaConstants.ItemID), ItemId);
            }
            else if (ItemId >= 161000001U && ItemId <= 161001000U)
            {
                try
                {
                    str1 = Enum.GetName(typeof(SchemaConstants.MagiciteID), ItemId);
                }
                catch (Exception ex)
                {
                    FiddlerApplication.Log.LogString(ex.ToString());
                }
            }
            else
            {
                str1 = !SchemaConstants.miscItemNames.ContainsKey(ItemId)
                    ? defaultName
                    : SchemaConstants.miscItemNames[ItemId];
            }

            var str3 = str1.Replace('_', ' ');
            if (num > 1U)
                str3 += string.Format(" x{0}", num);
            return str3;
        }

    }
}