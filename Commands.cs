using CommandSystem;
using Exiled.Permissions.Extensions;
using System;
using System.Linq;
using Utils;

namespace ReverseFriendlyFire
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ReverseFriendlyFireCommand : ParentCommand
    {
        public ReverseFriendlyFireCommand() => LoadGeneratedCommands();

        public override string Command { get; } = "rff";

        public override string[] Aliases => Plugin.PluginTranslation.ParentCommandAliases;

        public override string Description => Plugin.PluginTranslation.ParentCommandDescription;

        public static ReverseFriendlyFireCommand Create()
        {
            ReverseFriendlyFireCommand cmd = new ReverseFriendlyFireCommand();
            cmd.LoadGeneratedCommands();
            return cmd;
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = $"{Plugin.PluginTranslation.AvailableCommands}\n{string.Join("\n", AllCommands.Select(x => $"{Command} {x.Command} - {x.Description}"))}";
            return false;
        }

        public override void LoadGeneratedCommands()
        {
            RegisterCommand(new Toggle());
            RegisterCommand(new Info());
        }
    }

    [CommandHandler(typeof(ReverseFriendlyFireCommand))]
    class Toggle : ICommand
    {
        public string Command { get; } = "toggle";

        public string[] Aliases => Plugin.PluginTranslation.ToggleCommandAliases;

        public string Description => Plugin.PluginTranslation.ToggleCommandDescription;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission($"rff.{Command}"))
            {
                response = Plugin.PluginTranslation.InsufficientPermissions;
                return false;
            }

            response = Plugin.PluginTranslation.ToggleCommandResponse.Replace("%state%", (EventHandlers.RffEnabled = !EventHandlers.RffEnabled) ? Plugin.PluginTranslation.ToggleCommandRffEnabled : Plugin.PluginTranslation.ToggleCommandRffDisabled);
            return true;
        }
    }

    [CommandHandler(typeof(ReverseFriendlyFireCommand))]
    class Info : ICommand
    {
        public string Command { get; } = "info";

        public string[] Aliases => Plugin.PluginTranslation.InfoCommandAliases;

        public string Description => Plugin.PluginTranslation.InfoCommandDescription;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission($"rff.{Command}"))
            {
                response = Plugin.PluginTranslation.InsufficientPermissions;
                return false;
            }

            if (arguments.Count < 1)
            {
                response = Plugin.PluginTranslation.InfoCommandUsage;
                return false;
            }

            var hubs = RAUtils.ProcessPlayerIdOrNamesList(arguments, 0, out _);

            response = $"{Plugin.PluginTranslation.InfoCommandTableHeader}\n{string.Join("\n", hubs.Select(x => $"{x.nicknameSync.CombinedName} | {(EventHandlers.PlayerDataDictionary.TryGetValue(x.authManager.UserId, out var pd) ? $"{(pd.ReverseDamageActivated ? Plugin.PluginTranslation.InfoCommandTableReverseDamageEnabled : Plugin.PluginTranslation.InfoCommandTableReverseDamageDisabled)} | {pd.Kills} | {pd.Damage}" : Plugin.PluginTranslation.InfoCommandTableNotFound)}"))}";
            return true;
        }
    }
}