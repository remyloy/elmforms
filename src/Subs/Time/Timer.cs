using System;

namespace ElmForms.Subs.Time
{
    public static class Timer
    {
        public static Sub<T> Every<T>(TimeSpan tick, Tagger<T> tagger)
        {
            return new Sub<T>(new TimerEvent(tick), tagger);
        }
    }
}