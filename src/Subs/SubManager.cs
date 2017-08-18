using System;
using System.Collections.Generic;
using System.Linq;
using ElmForms.Subs.Time;
using Timer = System.Windows.Forms.Timer;

namespace ElmForms.Subs
{
    public sealed class SubManager<T>
    {
        private Action _dispose = () => { };
        private IReadOnlyList<Sub<T>> _subs = Sub.Empty<T>();

        public void Step(IReadOnlyList<Sub<T>> subs, Action<T> sendMessage)
        {
            if (!_subs.SequenceEqual(subs))
            {
                _dispose();
                _dispose = StepSubs(subs, sendMessage);
                _subs = subs;
            }
        }

        private static Action StepSubs(IReadOnlyList<Sub<T>> subs, Action<T> sendMessage)
        {
            var disposables = new List<Action>();
            foreach (var action in subs)
            {
                var disposable = action.Event.Match(
                    x => HandleTimerSub(x, a => sendMessage(action.Tagger(a))));
                disposables.Add(disposable);
            }
            return () => disposables.ForEach(x => x());
        }

        private static Action HandleTimerSub(TimerEvent x, Action<EventArgs> onTick)
        {
            void OnTimerOnTick(object sender, EventArgs args)
            {
                onTick(args);
            }

            var timer = new Timer {Interval = (int) x.TickRate.TotalMilliseconds};
            timer.Tick += OnTimerOnTick;
            timer.Start();
            return () =>
            {
                timer.Stop();
                timer.Tick -= OnTimerOnTick;
                timer.Dispose();
            };
        }
    }
}