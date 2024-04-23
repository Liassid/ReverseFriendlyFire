using System;

namespace ReverseFriendlyFire.Events
{
    public static class Events
    {
        public static event EventHandler<ReverseDamageActivatedEventArgs>? ReverseDamageActivated;
        internal static void OnReverseDamageActivated(ReverseDamageActivatedEventArgs ev) => ReverseDamageActivated?.Invoke(null, ev);
    }
}