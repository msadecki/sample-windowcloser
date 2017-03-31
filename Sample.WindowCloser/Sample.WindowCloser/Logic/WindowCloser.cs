using System;
using System.Diagnostics;
using System.Threading;

namespace Sample.WindowCloser.Logic
{
    /// <summary>
    /// Inspirations
    /// Google:
    /// find and close window c#
    /// find window by title c#
    /// http://stackoverflow.com/questions/13547639/return-window-handle-by-its-name-title
    /// https://support.microsoft.com/en-us/help/305603/how-to-use-visual-c-to-close-another-application
    /// https://www.codeproject.com/Articles/22257/Find-and-Close-the-Window-using-Win-API
    /// </summary>
    public class WindowCloser
    {
        private readonly Action<string> _showOutputAction;
        private readonly AutoResetEvent _stopEvent = new AutoResetEvent(false);
        private int _closedCounter;

        public WindowCloser(
            Action<string> showOutputAction)
        {
            _showOutputAction = showOutputAction;
        }

        public void StopWatching()
        {
            _stopEvent.Set();
        }

        public void StartWatchingToEnsureCloseWindow(string windowTitle, TimeSpan checkinterval)
        {
            do
            {
                EnsureCloseWindow(windowTitle);
            }
            while (!_stopEvent.WaitOne(checkinterval));
        }

        private void EnsureCloseWindow(string windowTitle)
        {
            foreach (var process in Process.GetProcesses())
            {
                if (process.MainWindowTitle.Equals(windowTitle))
                {
                    try
                    {
                        process.CloseMainWindow();
                        _closedCounter++;
                        _showOutputAction?.Invoke($"Closed counter: {_closedCounter}");
                    }
                    catch (Exception exception)
                    {
                        _showOutputAction?.Invoke(exception.Message);
                    }
                }
            }
        }
    }
}