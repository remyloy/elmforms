namespace ElmForms.Counters
{
    internal sealed class Model
    {
        public Model(int ticks)
        {
            Ticks = ticks;
        }

        public int Ticks { get; }
    }
}