using Exiled.API.Features;
using System;
using System.Reflection;

namespace ReverseFriendlyFire
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "ReverseFriendlyFire";
        public override string Author => "liassid";
        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;
        public override Version RequiredExiledVersion => new Version(8, 8, 1);

        public static Config PluginConfig;

        public override void OnEnabled()
        {
            PluginConfig = Config;

            EventHandlers.RegisterEvents();

            base.OnEnabled();
        }
    }
}