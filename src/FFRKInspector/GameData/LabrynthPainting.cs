using System.Collections.Generic;
using Newtonsoft.Json;

namespace FFRKInspector.GameData
{
    public class LabrynthPainting
    {
        [JsonProperty("painting_id")] public string PaintingId;
        [JsonProperty("name")]  public string Name;
        [JsonProperty("dungeon")] public LabrynthDungeon LabrynthDungeon;
        [JsonProperty("type")] public int Type;
    }

    public class LabrynthDungeon
    {
        [JsonProperty("captures")] public List<LabrynthCapture> LabrynthCapture;
    }

    public class LabrynthCapture
    {
        [JsonProperty("enemy_id")] public string EnemyId;
        [JsonProperty("image_path")] public string ImagePath;
    }
}