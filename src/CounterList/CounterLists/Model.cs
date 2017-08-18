using System.Collections.Generic;

namespace ElmForms.CounterLists
{
    internal sealed class Model
    {
        public Model(IReadOnlyList<Counters.Model> counters)
        {
            Counters = counters;
        }

        public IReadOnlyList<Counters.Model> Counters { get; }
    }
}