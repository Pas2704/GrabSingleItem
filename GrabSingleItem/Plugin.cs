using HarmonyLib;
using NLog;
using System.Reflection;
using VRage.Plugins;

namespace GrabSingleItem
{
    public class Plugin : IPlugin
    {
        internal static readonly Logger Log = LogManager.GetCurrentClassLogger();
        public void Init(object gameInstance)
        {
            Log.Debug("GrabSingleItem: Patching");
            new Harmony("GrabSingleItem").PatchAll(Assembly.GetExecutingAssembly());
            Log.Info("GrabSingleItem: Patches applied");
        }

        public void Dispose()
        {
        }

        public void Update()
        {
        }
    }
}
