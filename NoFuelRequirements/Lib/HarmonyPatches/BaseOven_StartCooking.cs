using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Harmony;
using NoFuelRequirements.Lib;
using NoFuelRequirements.Lib.Logging;
using UnityEngine;

namespace NoFuelRequirements.HarmonyPatches
{
    [HarmonyPatch(typeof(BaseOven), nameof(BaseOven.StartCooking))]
    public class BaseOven_StartCooking
    {
         /// <summary>
        /// Prepare the plugin's datasource for use and check if the plugin is enabled.
        /// </summary>
        public static bool Prepare(MethodBase original)
        {
            Main.CheckBootAndInit();
            return (bool)Main.instance.Config.Get("Enabled");
        }
        
        /// <summary>
        /// Modifies the original code instructions to modify the furnaces' fuel consumption.
        /// </summary>
        /// <param name="originalInstructions">Original code instructions to meddle with</param>
        /// <returns>Possibly modified code instructions to replace the original method's instructions</returns>
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> originalInstructions)
        {
            
            
            
            List<CodeInstruction> codeInstructionList = new List<CodeInstruction>(originalInstructions);

            try
            {
                
                int StartIndex = -1;
                int EndIndex = -1;
                
                foreach (CodeInstruction codeInstruction in codeInstructionList)
                {
                    
                    // check if the instruction is a call to the method "FindBurnable"
                    if (codeInstruction.opcode == OpCodes.Call && codeInstruction.operand is MethodInfo methodInfo &&
                        methodInfo.Name == "FindBurnable")
                    {
                        StartIndex = codeInstructionList.IndexOf(codeInstruction) - 1;
                        EndIndex = codeInstructionList.IndexOf(codeInstruction) + 2;
                        // log instruction and index
                        break;
                    }
                }
                
                if(StartIndex == -1 || EndIndex == -1)
                {
                    LoggingManager.Log("Failed to find FindBurnable requirement method");
                    return (IEnumerable<CodeInstruction>)codeInstructionList;
                }
                
                for (int i = StartIndex; i <= EndIndex; i++)
                {
                    codeInstructionList[i].opcode = OpCodes.Nop;
                    codeInstructionList[i].operand = null;
                }
            }
            catch (Exception e)
            {
                LoggingManager.Log($"Unable to patch baseoven startcooking: {e.Message}");
            }
            
            return (IEnumerable<CodeInstruction>)codeInstructionList;
        }
    }
}