using System;

namespace ElmForms.Subs.Time
{
    internal sealed class TimerEvent : IEvent, IEquatable<TimerEvent>
    {
        public TimerEvent(TimeSpan tickRate)
        {
            TickRate = tickRate;
        }

        public TimeSpan TickRate { get; }

        public bool Equals(TimerEvent other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return TickRate.Equals(other.TickRate);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is TimerEvent x && Equals(x);
        }

        public override int GetHashCode()
        {
            return TickRate.GetHashCode();
        }
    }
}