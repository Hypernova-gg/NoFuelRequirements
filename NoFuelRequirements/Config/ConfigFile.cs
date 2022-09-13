using System.Collections.Generic;
using Newtonsoft.Json;

namespace NoFuelRequirements.Config
{
    public class ConfigFile
    {
        [JsonProperty(PropertyName = "Modification enabled (true/false)")]
        public bool Enabled { get; set; } = true;
        
        [JsonProperty(PropertyName = "Affected prefabs (prefab shortname)", ObjectCreationHandling = ObjectCreationHandling.Replace)]
        public List<string> Prefabs { get; set; } = new List<string>()
        {
            "furnace",
            "furnace.large",
            "refinery_small_deployed",
            "campfire",
            "bbq.deployed",
            "skull_fire_pit"
        };
    }
}


            
