using BepInEx;
using System.IO;
using UnityEngine;

namespace EvesRainyExpansion
{
    #region Dependencies
    /*[BepInDependency("com.bepis.r2api.rules")]
    [BepInDependency("com.bepis.r2api.teams")]
    [BepInDependency("com.bepis.r2api.extendedStringSerializer")]
    [BepInDependency("com.bepis.r2api.stages")]
    [BepInDependency("com.bepis.r2api.addressables")]
    [BepInDependency("com.bepis.r2api.animations")]
    [BepInDependency("com.bepis.r2api.character_body")]
    [BepInDependency("com.bepis.r2api.skills")]
    [BepInDependency("com.bepis.r2api.proctype")]
    [BepInDependency("com.bepis.r2api.colors")]
    [BepInDependency("com.bepis.r2api.execute")]
    [BepInDependency("com.bepis.r2api.buffs")]*/
    [BepInDependency("com.bepis.r2api")]
    #endregion
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class EvesRainyExpansionMain : BaseUnityPlugin
    {
        public const string GUID = "com.obvEve.EvesRainyExpansion";
        public const string MODNAME = "Eves Rainy Expansion";
        public const string VERSION = "0.0.1";

        public static EvesRainyExpansionMain Instance { get; private set; }
        public static PluginInfo PluginInfo => Instance.Info;
        internal static AssetBundle AssetBundle { get; private set; }
        internal static string AssetBundleDir => Path.Combine(Path.GetDirectoryName(PluginInfo.Location)!, "EvesRainyExpansionAssets");

        private void Awake()
        {
            Instance = this;
            _ = new EvesContentProvider();
        }

        internal static void LogFatal(object data)
        {
            Instance.Logger.LogFatal(data);
        }

        internal static void LogError(object data)
        {
            Instance.Logger.LogError(data);
        }

        internal static void LogWarning(object data)
        {
            Instance.Logger.LogWarning(data);
        }

        internal static void LogMessage(object data)
        {
            Instance.Logger.LogMessage(data);
        }

        internal static void LogInfo(object data)
        {
            Instance.Logger.LogInfo(data);
        }

        internal static void LogDebug(object data)
        {
            Instance.Logger.LogDebug(data);
        }
    }
}
