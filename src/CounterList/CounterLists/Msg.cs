namespace ElmForms.CounterLists
{
    internal abstract class Msg
    {
        public static Msg ToCounter(int index, Counters.Msg msg)
        {
            return new ToCounter(index, msg);
        }
    }

    internal sealed class ToCounter : Msg
    {
        public ToCounter(int index, Counters.Msg msg)
        {
            Index = index;
            Msg = msg;
        }

        public int Index { get; }
        public Counters.Msg Msg { get; }
    }
}