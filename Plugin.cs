using Exiled.API.Features;
using System;

namespace ReverseFriendlyFire
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "ReverseFriendlyFire";
        public override string Author => "liassid";
        public override Version Version => new Version(1, 0, 0);
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