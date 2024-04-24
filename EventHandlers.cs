using Exiled.API.Features;
using Exiled.API.Features.DamageHandlers;
using Exiled.Events.EventArgs.Player;
using PlayerRoles;
using ReverseFriendlyFire.Events;
using System.Collections.Generic;

namespace ReverseFriendlyFire
{
    public static class EventHandlers
    {
        public static bool RffEnabled;
        public static Dictionary<string, PlayerData> PlayerDataDictionary = new();

        public static void RegisterEvents()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers += OnWaitingForPlayers;
            Exiled.Events.Handlers.Player.Verified += OnVerified;
            Exiled.Events.Handlers.Player.Hurting += OnHurting;
            Exiled.Events.Handlers.Player.Dying += OnDying;
        }

        public static void OnWaitingForPlayers()
        {
            RffEnabled = Plugin.PluginConfig.DefaultState;
            PlayerDataDictionary.Clear();
        }

        public static void OnVerified(VerifiedEventArgs ev)
        {
            if (PlayerDataDictionary.TryGetValue(ev.Player.UserId, out var pd))
            {
                if (pd.ReverseDamageActivated)
                {
                    ev.Player.ShowHint(Plugin.PluginTranslation.ReverseDamageActivationHint);
                    Log.Debug($"{ev.Player.CustomName} ({ev.Player.UserId}) has damage reversing activated");
                }
                return;
            }

            PlayerDataDictionary[ev.Player.UserId] = new();
            Log.Debug($"{ev.Player.CustomName} ({ev.Player.UserId}) added to dictionary");
        }

        public static void OnHurting(HurtingEventArgs ev)
        {
            if (!Server.FriendlyFire || !RffEnabled || !ev.IsAllowed || ev.DamageHandler is not AttackerDamageHandler adh || !adh.AttackerFootprint.IsSet || adh.AttackerFootprint.NetId == adh.TargetFootprint.NetId || adh.AttackerFootprint.Role.GetFaction() != adh.TargetFootprint.Role.GetFaction() || !PlayerDataDictionary.TryGetValue(adh.AttackerFootprint.LogUserID, out var pd) || (Plugin.PluginConfig.EnableImmunityPermission && ServerStatic.PermissionsHandler._members.TryGetValue(adh.AttackerFootprint.LogUserID, out var groupName) && ServerStatic.PermissionsHandler._groups.TryGetValue(groupName, out var group) && PermissionsHandler.IsPermitted(group.Permissions, PlayerPermissions.FriendlyFireDetectorImmunity)))
                return;

            if (pd.ReverseDamageActivated)
            {
                ev.IsAllowed = false;
                ev.Attacker?.Hurt(adh.Damage, Plugin.PluginTranslation.ReverseDamageReason);
                Log.Debug($"{adh.AttackerFootprint.Nickname} ({adh.AttackerFootprint.LogUserID})'s damage ({adh.Damage}) to {adh.TargetFootprint.Nickname} ({adh.TargetFootprint.LogUserID}) has been reversed");
                return;
            }

            var multiplier = Plugin.PluginConfig.DamageTypeMultipliers.TryGetValue(adh.Type, out var mult) ? mult.Damage : 1f;

            if ((pd.Damage += adh.Damage * multiplier) >= Plugin.PluginConfig.DamageThreshold)
            {
                pd.ReverseDamageActivated = true;
                ev.Attacker?.ShowHint(Plugin.PluginTranslation.ReverseDamageActivationHint);
                Events.Events.OnReverseDamageActivated(new ReverseDamageActivatedEventArgs(adh.AttackerFootprint.LogUserID, ReverseDamageActivationReason.Damage, pd));
                Log.Debug($"{adh.AttackerFootprint.Nickname} ({adh.AttackerFootprint.LogUserID})'s damage ({pd.Damage}/{Plugin.PluginConfig.DamageThreshold}, {adh.Damage}, {multiplier}) to {adh.TargetFootprint.Nickname} ({adh.TargetFootprint.LogUserID}) activated damage reversing");
            }
        }

        public static void OnDying(DyingEventArgs ev)
        {
            if (!Server.FriendlyFire || !RffEnabled || !ev.IsAllowed || ev.DamageHandler is not AttackerDamageHandler adh || !adh.AttackerFootprint.IsSet || adh.AttackerFootprint.NetId == adh.TargetFootprint.NetId || adh.AttackerFootprint.Role.GetFaction() != adh.TargetFootprint.Role.GetFaction() || !PlayerDataDictionary.TryGetValue(adh.AttackerFootprint.LogUserID, out var pd) || pd.ReverseDamageActivated || (Plugin.PluginConfig.EnableImmunityPermission && ServerStatic.PermissionsHandler._members.TryGetValue(adh.AttackerFootprint.LogUserID, out var groupName) && ServerStatic.PermissionsHandler._groups.TryGetValue(groupName, out var group) && PermissionsHandler.IsPermitted(group.Permissions, PlayerPermissions.FriendlyFireDetectorImmunity)))
                return;

            var multiplier = Plugin.PluginConfig.DamageTypeMultipliers.TryGetValue(adh.Type, out var mult) ? mult.Kills : 1f;

            if ((pd.Kills += multiplier) >= Plugin.PluginConfig.KillsThreshold)
            {
                pd.ReverseDamageActivated = true;
                ev.Attacker?.ShowHint(Plugin.PluginTranslation.ReverseDamageActivationHint);
                Events.Events.OnReverseDamageActivated(new ReverseDamageActivatedEventArgs(adh.AttackerFootprint.LogUserID, ReverseDamageActivationReason.Kill, pd));
                Log.Debug($"{adh.AttackerFootprint.Nickname} ({adh.AttackerFootprint.LogUserID})'s kill ({pd.Kills}/{Plugin.PluginConfig.KillsThreshold}, {multiplier}) of {adh.TargetFootprint.Nickname} ({adh.TargetFootprint.LogUserID}) activated damage reversing");
            }
        }
    }
}