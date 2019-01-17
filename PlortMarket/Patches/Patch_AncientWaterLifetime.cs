using System;
using System.Collections.Generic;
using Harmony;
using System.Reflection.Emit;
using UModFramework.API;

namespace PlortMarket.Patches
{
    [HarmonyPatch(typeof(Ammo))]
    [HarmonyPatch("MaybeAddToSlot")]
    class Patch_AncientWaterLifetime
    {
        static bool isAncientWater;

        static void Prefix(Ammo __instance, Identifiable.Id id)
        {
            PlortMarket.Log("[AncientWater-Prefix] id: " + id.ToString());
            if (id == Identifiable.Id.MAGIC_WATER_LIQUID)
            {
                PlortMarket.Log("[AncientWater-Prefix] ID == MAGIC_WATER_LIQUID");
                isAncientWater = true;
            }
            else
            {
                //PlortMarket.Log("[AncientWater-Prefix] ID != MAGIC_WATER_LIQUID");
                isAncientWater = false;
            }
        }
        static void Postfix(ref double ___waterIsMagicUntil)
        {
            if ( isAncientWater == true)
            {
                PlortMarket.Log("[AncientWater-Postfix] isAncientWater == TRUE");
                ___waterIsMagicUntil = SRSingleton<SceneContext>.Instance.TimeDirector.HoursFromNow(PlortMarketConfig.Instance.AncientWaterLifetime);
                PlortMarket.Log("[AncientWater-Postfix] ___waterIsMagicUntil:");
            }
            else
            {
                PlortMarket.Log("[AncientWater-Postfix] isAncientWater == FALSE");
            }
        }
        
    }
}
