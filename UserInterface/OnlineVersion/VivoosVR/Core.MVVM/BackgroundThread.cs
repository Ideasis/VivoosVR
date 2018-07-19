using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MVVM
{
    public class BackgroundThread
    {
        BackgroundWorker _Worker;

        public Action<RunWorkerCompletedEventArgs> CompletedAction { get; set; }
        public Action<DoWorkEventArgs> ExecuteAction { get; set; }
        public Action<ProgressChangedEventArgs> ProgressAction { get; set; }

        public BackgroundThread()
        {
            _Worker = new BackgroundWorker();
            _Worker.WorkerReportsProgress = true;
        }

        private void _Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressAction(e);
        }

        public void ReportProgress(int percentProgress)
        {
            if (_Worker.IsBusy)
            {
                _Worker.ReportProgress(percentProgress);
            }
        }

        public void Run()
        {
            _Worker.RunWorkerAsync();
        }

        public void Run(object argument)
        {
            _Worker.RunWorkerAsync(argument);
        }

        public void Subscribe(Action<DoWorkEventArgs> executeAction)
        {
            ExecuteAction = executeAction;
            _Worker.DoWork += Worker_DoWork;
        }

        public void SubscribeCompleted(Action<RunWorkerCompletedEventArgs> completedAction)
        {
            CompletedAction = completedAction;
            _Worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        }

        public void SubscribeProgress(Action<ProgressChangedEventArgs> progressAction)
        {
            ProgressAction = progressAction;
            _Worker.ProgressChanged += _Worker_ProgressChanged;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ExecuteAction(e);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CompletedAction(e);
        }
    }
}
