using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FFRKInspector.GameData
{
    public class LabrynthSessionData
    {
        [JsonProperty("labyrinth_dungeon_session")]
        public CurrentLabrynthSession LabrynthSession;
        [JsonExtensionData]
        public Dictionary<string, JToken> UnknownValues;
    }
}