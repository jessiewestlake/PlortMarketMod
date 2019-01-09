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

        static void Prefix(Ammo __instance, Identifiable.Id id, ref bool __state)
        {
            PlortMarket.Log("[AncientWater-Prefix] id: " + id.ToString());
            if (id == Identifiable.Id.MAGIC_WATER_LIQUID)
            {
                //PlortMarket.Log("[AncientWater-Prefix] id macthes MAGIC_WATER_LIQUID? TRUE");
                __state = true;
            }
            else
            {
                //PlortMarket.Log("[AncientWater-Prefix] id macthes MAGIC_WATER_LIQUID? FALSE");
                __state = false;
            }
        }
        static void Postfix(ref double ___waterIsMagicUntil, bool __state)
        {
            if ( __state == true)
            {
                PlortMarket.Log("[AncientWater-Postfix] __state = TRUE");
                PlortMarket.Log("[AncientWater-Postfix] id macthes MAGIC_WATER_LIQUID? True");
                ___waterIsMagicUntil = SRSingleton<SceneContext>.Instance.TimeDirector.HoursFromNow(PlortMarketConfig.Instance.AncientWaterLifetime);
                PlortMarket.Log("[AncientWater-Postfix] ___waterIsMagicUntil:");
            }
            else
            {
                PlortMarket.Log("[AncientWater-Postfix] __state = FALSE");
            }
        }
        
    }
}
