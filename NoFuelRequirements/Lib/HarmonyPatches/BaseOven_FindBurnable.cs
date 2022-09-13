using System.Collections.Generic;
using System.Reflection;
using Harmony;
using NoFuelRequirements.Lib;
using NoFuelRequirements.Lib.Logging;
using UnityEngine;

namespace NoFuelRequirements.HarmonyPatches
{
    public class BaseOven_FindBurnable
    {
        /// <summary>
        /// Prepare the plugin's datasource for use and check if the plugin is enabled.
        /// </summary>
        public static bool Prepare(MethodBase original)
        {
            Main.CheckBootAndInit();
            return (bool)Main.instance.Config.Get("Enabled");
        }
        
        // FindBurnable
        [HarmonyPatch(typeof(BaseOven), "FindBurnable")]
        public class Patch
        {
            
            static void Postfix(BaseOven __instance, ref Item __result)
            {
                
                List<string> affectedPrefabs = (List<string>)Main.instance.Config.Get("Prefabs");
                if (!affectedPrefabs.Contains(__instance.ShortPrefabName))
                    return; // Not affected by this plugin
                
                if (__result != null)
                    return; // if we already have a result, don't do anything, we should allow vanilla behavior if desired
                
                // check if the oven has an item in the firemod slot
                Item burnItem = Helpers.OvenHasCookable(__instance);
                if (burnItem == null)
                {
                    // if it doesn't, we should let the game decide to either stop cooking or burn the fuel item
                    return;
                }
                
                // Looks like the oven has a cookable inserted, so we should return the "infinite" fuel item
                var item = ItemManager.Create(ItemManager.FindItemDefinition("wood"), 10000);
                item.info.GetComponent<ItemModBurnable>().byproductItem = null; // Disable the depositing of Charcoal if the item is burned
                __result = item;
                
            }
        }
    }
}