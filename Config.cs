using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace ReverseFriendlyFire
{
    public sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        [Description("Should RFF be enabled at the start of the round?")]
        public bool DefaultState { get; set; } = true;
        [Description("Should players with FriendlyFireDetectorImmunity permission be ignored?")]
        public bool EnableImmunityPermission { get; set; } = false;
        [Description("What amount of teamkills should activate Damage Reversing?")]
        public float KillsThreshold { get; set; } = 1f;
        [Description("What amount of damage to teammates should activate Damage Reversing?")]
        public float DamageThreshold { get; set; } = 500f;
        [Description("What multipliers should be applied on kills and damages with specified Damage Types? E.g. 0.34 kill multiplier will result in Damage Reversing activating only on 3rd kill with SCP-018")]
        public Dictionary<DamageType, DamageTypeMultiplier> DamageTypeMultipliers { get; set; } = new() { { DamageType.Scp018, new() { Kills = 0.34f, Damage = 0.34f } } };
        [Description("What death message should be shown to player when dying due to Damage Reversing?")]
        public string ReverseDamageReason { get; set; } = "Teamkilling";
        [Description("What hint should shown be to player when Damage Reversing is activated?")]
        public Hint ReverseDamageActivationHint { get; set; } = new() { Content = "<color=red>Reverse Friendly Fire activated</color>\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n", Duration = 5f };
    }
}