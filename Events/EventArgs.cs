namespace ReverseFriendlyFire.Events
{
    public sealed class ReverseDamageActivatedEventArgs
    {
        public ReverseDamageActivatedEventArgs(string userId, ReverseDamageActivationReason reason, PlayerData data)
        {
            UserId = userId;
            Reason = reason;
            Data = data;
        }

        public string UserId { get; }
        public ReverseDamageActivationReason Reason { get; }
        public PlayerData Data { get; }
    }

    public enum ReverseDamageActivationReason
    {
        Damage,
        Kill
    }
}