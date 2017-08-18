using System.Windows.Forms;
using ElmForms.CounterLists;
using ElmForms.Subs;

namespace ElmForms
{
    public partial class CounterListForm : Form
    {
        private readonly SubManager<Msg> _subManager;
        private Model _model;

        public CounterListForm()
        {
            InitializeComponent();

            _subManager = new SubManager<Msg>();
            _model = CounterList.Init();
            label.Text = CounterList.View(_model);
            var subs = CounterList.Subs(_model);
            _subManager.Step(subs, Update);
        }

        private void Update(Msg msg)
        {
            _model = CounterList.Update(_model, msg);
            label.Text = CounterList.View(_model);
            var subs = CounterList.Subs(_model);
            _subManager.Step(subs, Update);
        }
    }
}