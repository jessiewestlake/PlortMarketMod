using Harmony;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PlortMarket.Patches
{
    [HarmonyPatch(typeof(MonomiPark.SlimeRancher.DataModel.PlayerModel))]
    [HarmonyPatch("ApplyUpgrade")]
    class Patch_HealthUpgrade
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            int num = 0;
            bool found1 = false;
            bool patched = false;
            foreach (var instruction in instructions)
            {
                //Wait until the 4th instance of defaultMaxHealth is found
                if (!found1 && instruction.opcode.Equals(OpCodes.Ldfld) && instruction.operand.ToString().Contains("maxHealth"))
                {
                    //After the third instance we know the 4th one is our health upgrade based on the code.
                    if (num != 6) num++;
                    else
                    {
                        PlortMarket.Log("HealthUpgrade1: Found maxHealth");
                        found1 = true;
                        //Since it's found we quickly leave the instruction alone and move onto the next instruction.
                        yield return instruction;
                        continue;
                    }
                }
                //The actual patch will not happen until both found1 is set.
                if (!patched && found1 && instruction.opcode.Equals(OpCodes.Ldc_R4) && instruction.operand.Equals(350f))
                {
                    PlortMarket.Log("HealthUpgrade1: Found all and patched!");
                    patched = true;
                    //This is where we replace the ldc.r4 3.5 value with our own from the config setting.
                    PlortMarket.Log("HealthUpgrade1: Instruction = " + instruction.ToString());
                    CodeInstruction myInstruction = new CodeInstruction(OpCodes.Ldc_R4, PlortMarketConfig.Instance.HealthUpgrade4);
                    PlortMarket.Log("HealthUpgrade1: myInstruction = " + myInstruction.ToString());
                    yield return myInstruction;
                    continue;
                }
                //This leaves the opcode and operand unchanged if none of the above conditions are met.
                yield return instruction;
            }
        }
    }
}