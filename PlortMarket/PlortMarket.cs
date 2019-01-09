using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UModFramework.API;

namespace PlortMarket
{
    public class PlortMarket
    {
        //The [UMFHarmony] attribute on a method/function instructs UMF to:
        //1. Start the method this attribute is attached to. (If attached to a class instead of a method it will start anything.)
        //2. Try run the method that the [UMFConfig] attribute is attached to if it exists. It will wait until this is done before doing the next step.
        //3. Create a UMF managed harmony instance and run all patches contained within this mod.
        //The first optional parameter which is set to 1 is the number of Harmony patches your mod has.
        //This is used to count and compare the number of patches that applied with the number of patches the mod supplies,
        //and if they are not matching the user will be warned that the mod may be outdated and should avoid using it.
        //The true paramter also enables the harmony debug log which can be found with the other logs. REMOVE this paramter for release builds you are distributing.
        [UMFHarmony(1, true)]
        public static void Start()
        {
            //This is the first log entry for your mod, you use the additional true parameter to tell the logger to clean the log file here.
            Log("Plort Market v" + UMFMod.GetModVersion().ToString(), true);
        }

        //The [UMFConfig] attribute is used to identify which method reloads the configs of the mod.
        //UMF will run this method when:
        //1. During mod startup if the [UMFHarmony] attribute is used.
        //2. When the user changes or resets settings from inside the game.
        //3. When certain console commands that modify the config are used.
        //4. In the future it will also be used for more config related things.
        [UMFConfig]
        public static void LoadConfig()
        {
            //Rather than storing all your configs in this method you should save them in their own class to keep things neat and orderly.
            //So instead we tell the config instance to load the settings from here.
            PlortMarketConfig.Instance.Load();
        }

        //This is a standard log function for UMF that you will want to use for all your mods.
        //Whenever you want to log something, simply use MyFirstMod.Log("My log text");
        internal static void Log(string text, bool clean = false)
        {
            using (UMFLog log = new UMFLog()) log.Log(text, clean);
        }
    }
}
