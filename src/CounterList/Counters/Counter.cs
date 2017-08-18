using System;
using System.Collections.Generic;
using ElmForms.Subs;
using ElmForms.Subs.Time;

namespace ElmForms.Counters
{
    internal static class Counter
    {
        public static Model Init()
        {
            return new Model(0);
        }

        public static IReadOnlyList<Sub<Msg>> Subs(Model model)
        {
            if (model.Ticks < 10)
                return Sub.From(Timer.Every(TimeSpan.FromSeconds(0.5), x => Msg.Tick()));

            if (model.Ticks < 20)
                return Sub.From(Timer.Every(TimeSpan.FromSeconds(1.0), x => Msg.Tick()));

            return Sub.Empty<Msg>();
        }

        public static Model Update(Model model, Msg msg)
        {
            return msg.Match(m => new Model(model.Ticks + 1));
        }

        public static string View(Model model)
        {
            return $"Ticks: {model.Ticks}";
        }

        private static T Match<T>(this Msg msg, Func<Tick, T> onTick)
        {
            switch (msg)
            {
                case Tick a: return onTick(a);
                default: throw new ArgumentException();
            }
        }
    }
}