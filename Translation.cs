using Exiled.API.Features;
using Exiled.API.Interfaces;
namespace ReverseFriendlyFire
{
    public class Translation : ITranslation
    {
        public string ReverseDamageReason { get; set; } = "You've been killed for teamkilling";
        public Hint ReverseDamageActivationHint { get; set; } = new() { Content = "<color=red>Reverse Friendly Fire activated</color>\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n", Duration = 5f };
        public string ParentCommandDescription { get; set; } = "Reverse Friendly Fire management";
        public string[] ParentCommandAliases { get; set; } = { };
        public string ToggleCommandDescription { get; set; } = "enabling/disabling RFF";
        public string[] ToggleCommandAliases { get; set; } = { "tg" };
        public string ToggleCommandResponse { get; set; } = "RFF has been %state%";
        public string ToggleCommandRffEnabled { get; set; } = "enabled";
        public string ToggleCommandRffDisabled { get; set; } = "disabled";
        public string InfoCommandDescription { get; set; } = "checking players' kills and damage";
        public string[] InfoCommandAliases { get; set; } = { "i" };
        public string InfoCommandUsage { get; set; } = "Usage: rff info <players>";
        public string InfoCommandTableHeader { get; set; } = "Nickname | State | Kills | Damage";
        public string InfoCommandTableReverseDamageEnabled { get; set; } = "Enabled";
        public string InfoCommandTableReverseDamageDisabled { get; set; } = "Disabled";
        public string InfoCommandTableNotFound { get; set; } = "Not found";
        public string AvailableCommands { get; set; } = "Available commands:";
        public string InsufficientPermissions { get; set; } = "Insufficient permissions";
    }
}