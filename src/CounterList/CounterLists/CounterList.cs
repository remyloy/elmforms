using System;
using System.Collections.Generic;
using System.Linq;
using ElmForms.Counters;
using ElmForms.Subs;

namespace ElmForms.CounterLists
{
    internal static class CounterList
    {
        public static Model Init()
        {
            return new Model(new[]
            {
                Counter.Init(),
                Counter.Init(),
                Counter.Init()
            });
        }

        public static Model Update(Model model, Msg msg)
        {
            return msg.Match(m =>
            {
                var counter = model.Counters[m.Index];
                var nextCounter = Counter.Update(counter, m.Msg);
                var nextList = model.Counters.ToArray();
                nextList[m.Index] = nextCounter;
                return new Model(nextList);
            });
        }

        public static IReadOnlyList<Sub<Msg>> Subs(Model model)
        {
            return model.Counters
                .SelectMany((c, i) => Counter.Subs(c).Select(y => y.Select(x => Msg.ToCounter(i, x))))
                .ToArray();
        }

        public static string View(Model model)
        {
            return string.Join(Environment.NewLine, model.Counters.Select(Counter.View));
        }

        private static T Match<T>(this Msg args, Func<ToCounter, T> onToCounter)
        {
            switch (args)
            {
                case ToCounter a: return onToCounter(a);
                default: throw new ArgumentException();
            }
        }
    }
}