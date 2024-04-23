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

        public override string[] Aliases { get; } = Array.Empty<string>();

        public override string Description { get; } = "Reverse Friendly Fire management";

        public static ReverseFriendlyFireCommand Create()
        {
            ReverseFriendlyFireCommand cmd = new ReverseFriendlyFireCommand();
            cmd.LoadGeneratedCommands();
            return cmd;
        }

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = $"Available commands:\nrff toggle - enabling/disabling RFF\nrff info <players> - checking players' kills and damage";
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

        public string[] Aliases { get; } = Array.Empty<string>();

        public string Description { get; } = null;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission($"rff.{Command}"))
            {
                response = "Insufficient permissions";
                return false;
            }

            response = $"RFF has been {((EventHandlers.RffEnabled = !EventHandlers.RffEnabled) ? "enabled" : "disabled")}";
            return true;
        }
    }

    [CommandHandler(typeof(ReverseFriendlyFireCommand))]
    class Info : ICommand
    {
        public string Command { get; } = "info";

        public string[] Aliases { get; } = Array.Empty<string>();

        public string Description { get; } = null;

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission($"rff.{Command}"))
            {
                response = "Insufficient permissions";
                return false;
            }

            if (arguments.Count < 1)
            {
                response = "Usage: rff info <players>";
                return false;
            }

            var hubs = RAUtils.ProcessPlayerIdOrNamesList(arguments, 0, out _);

            response = $"Nickname | State | Kills | Damage\n{string.Join("\n", hubs.Select(x => $"{x.nicknameSync.CombinedName} | {(EventHandlers.PlayerDataDictionary.TryGetValue(x.authManager.UserId, out var pd) ? $"{(pd.ReverseDamageActivated ? "Enabled" : "Disabled")} | {pd.Kills} | {pd.Damage}" : "Not found")}"))}";
            return true;
        }
    }
}