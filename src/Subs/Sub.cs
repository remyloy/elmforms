using System;
using System.Collections.Generic;
using ElmForms.Subs.Time;

namespace ElmForms.Subs
{
    public sealed class Sub<T> : IEquatable<Sub<T>>
    {
        internal Sub(IEvent @event, Tagger<T> tagger)
        {
            Tagger = tagger;
            Event = @event;
        }

        internal IEvent Event { get; }

        internal Tagger<T> Tagger { get; }

        public bool Equals(Sub<T> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Event.Equals(other.Event);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Sub<T> x && Equals(x);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Event.GetHashCode() * 397;
            }
        }
    }

    public static class Sub
    {
        public static IReadOnlyList<Sub<T>> Empty<T>()
        {
            return new Sub<T>[0];
        }

        public static IReadOnlyList<Sub<T>> From<T>(params Sub<T>[] subs)
        {
            return subs;
        }

        public static Sub<T2> Select<T1, T2>(this Sub<T1> sub, Func<T1, T2> selector)
        {
            return new Sub<T2>(sub.Event, args => selector(sub.Tagger(args)));
        }

        internal static T Match<T>(this IEvent @event, Func<TimerEvent, T> onTimer)
        {
            switch (@event)
            {
                case TimerEvent e: return onTimer(e);
                default: throw new ArgumentException();
            }
        }
    }
}