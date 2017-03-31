using System;
using System.Windows.Forms;
using Sample.WindowCloser.View;

namespace Sample.WindowCloser
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WindowCloserForm());
        }
    }
}
