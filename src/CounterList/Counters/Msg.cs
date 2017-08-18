namespace ElmForms.Counters
{
    internal abstract class Msg
    {
        public static Msg Tick()
        {
            return new Tick();
        }
    }

    internal sealed class Tick : Msg
    {
    }
}