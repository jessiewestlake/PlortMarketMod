using System;
using System.Collections.Generic;
using Harmony;
using System.Reflection.Emit;
using UModFramework.API;

namespace PlortMarket.Patches
{
    [HarmonyPatch(typeof(AirNet))]
    [HarmonyPatch("OnCollisionEnter")]
    class Patch_AirNetStrength
    {
        static bool Prefix(AirNet __instance, float ___netStrength)
        {
            ___netStrength = 1f;
            return false;
        }
    }
}
