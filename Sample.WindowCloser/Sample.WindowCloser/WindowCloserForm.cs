using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample.WindowCloser
{
    public partial class WindowCloserForm : Form
    {
        private readonly WindowCloser _windowCloser;

        public WindowCloserForm()
        {
            InitializeComponent();

            buttonStop.Enabled = false;

            _windowCloser = new WindowCloser(ShowOutput);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false;
            textBoxWindowTitleToClose.Enabled = false;

            Task.Run(() => _windowCloser.StartWatchingToEnsureCloseWindow(textBoxWindowTitleToClose.Text, TimeSpan.FromSeconds(1)));

            buttonStop.Enabled = true;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            buttonStop.Enabled = false;

            _windowCloser.StopWatching();

            buttonStart.Enabled = true;
            textBoxWindowTitleToClose.Enabled = true;
        }

        private void ShowOutput(string text)
        {
            if (textBoxOutput.InvokeRequired)
            {
                textBoxOutput.Invoke(new Action(() => ShowOutput(text)));
            }
            else
            {
                textBoxOutput.Text = $@"[{DateTime.Now}]{Environment.NewLine}{text}";
            }
        }
    }
}
