using System.Collections.Generic;
using Newtonsoft.Json;

namespace FFRKInspector.GameData
{
    public class CurrentLabrynthSession
    {
        [JsonProperty("display_paintings")]
        public List<LabrynthPainting> LabrynthPaintings;

        [JsonProperty("treasure_chest_ids")] public List<int> ChestIds;
    }
}