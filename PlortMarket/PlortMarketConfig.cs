using System;
using System.Collections.Generic;
using UModFramework.API;


namespace PlortMarket
{
    class PlortMarketConfig
    {
        //This is the config version used to delete the config file if the user's config is outdated.
        //Do not update this config version with every new release, instead only use it if you have changed setting names, setting defaults, etc,
        //to avoid annoying the user with having their settings reset all the time. You DO NOT need to use it for when new settings are added.
        //If you do not need a config version you can remove any code related to it.
        private static readonly string configVersion = "1.15";

        public float HealthUpgrade4;
        public float AncientWaterLifetime;

        public void Load()
        {
            //Logging is important in case you do something wrong.
            //We are logging before we log the settings, after we finish loading the UMF specific settings and when we have finished loading the settings.
            //We are also catching errors using the try catch clause and logging them.
            //All this logging will help you figure out exactly in which part something went wrong.
            PlortMarket.Log("Loading settings.");
            try
            {
                //Here we are actually doing all the config changes to the ini file where the settings are stored.
                //This is using an IDisposible in a using clause which is automatically disposed at the end, which basically means the ini file is no longer locked by the mod.
                using (UMFConfig cfg = new UMFConfig())
                {
                    //Config version related code
                    string cfgVer = cfg.Read("ConfigVersion", new UMFConfigString());
                    if (cfgVer != string.Empty && cfgVer != configVersion)
                    {
                        cfg.DeleteConfig();
                        PlortMarket.Log("The config file was outdated and has been deleted. A new config will be generated.");
                    }

                    //This is the UMF specific settings for the mod, these are not shown in game and are mostly set by the mod creator.
                    //Setting enabled to false means preventing the mod from being loaded by UMF.
                    //The load priority can be First, Normal and Last, and is used to ensure your mods that depend on each other work correctly.
                    //Min and max version is used to set which version of UMF the mod requires, and which version it might stop working again.
                    //UpdateURL is used to make your mod auto update it self when a new version is out. The response or contents should be "1.0|https://mymodwebsite.com/PlortMarket_v1.0.zip" without the quotes.
                    //ConfigVersion is as explained above.
                    //Read is used to only read a variable, and set the default if it does not exist.
                    //Write is used to always write the variable, regardless of if it exists or not.
                    cfg.Read("Enabled", new UMFConfigBool(true));
                    cfg.Read("LoadPriority", new UMFConfigString("Normal"));
                    cfg.Write("MinVersion", new UMFConfigString("0.45"));
                    cfg.Write("MaxVersion", new UMFConfigString("0.49.99999.99999"));
                    cfg.Write("UpdateURL", new UMFConfigString(@"https://mymodwebsite.com/mymodversion.txt"));
                    cfg.Write("ConfigVersion", new UMFConfigString(configVersion));

                    PlortMarket.Log("Finished UMF Settings.");

                    //Here we actually load all the mod's ini settings into their respective variables.
                    //You can read more about the UMFConfigParsers in the API section of this documentation.
                    //MyFirstIntSetting = cfg.Read("MyFirstIntSetting ", new UMFConfigInt(10, 0, 100, 1), "A description of what my int setting does.");
                    //MyFirstBoolSetting = cfg.Read("MyFirstBoolSetting ", new UMFConfigBool(true, false), "A description of what my bool setting does.");
                    
                    float health4_default = 500f;
                    float health4_vanilla = 350f;
                    float health4_rangeStart = 350f;
                    float health4_rangeEnd = 1000f;
                    //HealthUpgrade4 = cfg.Read("HealthUpgrade4", new UMFConfigInt(health4_default, health4_rangeStart, health4_rangeEnd, health4_vanilla), "The health level for the 4th Health Upgrade.");
                    HealthUpgrade4 = cfg.Read("HealthUpgrade4", new UMFConfigFloat(health4_default, health4_rangeStart, health4_rangeEnd, 1, health4_vanilla), "The health level for the 4th Health Upgrade.");
                    float ancientWater_default = 1.0f;
                    float ancientWater_rangeStart = 0.5f;
                    float ancientWater_rangeEnd = 10.0f;
                    float ancientWater_vanilla = 0.5f;
                    AncientWaterLifetime = cfg.Read("AncientWaterLifetime", new UMFConfigFloat(ancientWater_default, ancientWater_rangeStart, ancientWater_rangeEnd, 1, ancientWater_vanilla), "Time modifier for the Ancient Water effect.");
                    PlortMarket.Log("Finished loading settings.");
                }
            }
            catch (Exception e)
            {
                PlortMarket.Log("Error loading mod settings: " + e.Message + " (" + e.InnerException.Message + ")");
            }
        }

        //Leave this part alone unless to rename your classes.
        //This part is used to instance your config class so that it can be used in all places of the game you are modding.
        public static PlortMarketConfig Instance { get; } = new PlortMarketConfig();
    }
}
