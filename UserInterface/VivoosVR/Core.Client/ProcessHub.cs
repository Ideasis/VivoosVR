using Core.Client.Messages;
using Core.MVVM;
using Core.MVVM.MessageBox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Core.Client
{
    public class ProcessHub
    {
        Process _Process;

        public Action<MainWindowCloseMessage, Process> ApplicationClosingAction { get; set; }
        public Action<Process> ExitAction { get; set; }

        public ProcessHub()
        {
            _Process = new Process();

        }

        private void _Process_Exited(object sender, EventArgs e)
        {
            ExitAction(this._Process);
        }

        public void Start(string path)
        {
            ProcessStartInfo info = new ProcessStartInfo(path);
            info.Domain = "IdeaSis";
            _Process.StartInfo = info;
            _Process.EnableRaisingEvents = true;

            _Process.Start();

            //_Process.PriorityBoostEnabled = true;
            //_Process.PriorityClass = ProcessPriorityClass.High;


        }

        //public void SubscribeApplicationExit(Action<MainWindowCloseMessage, Process> applicationClosingAction)
        //{
        //    ApplicationClosingAction = applicationClosingAction;
        //    RxMessageBus.Subscribe<MainWindowCloseMessage>(x =>
        //    {
        //        ApplicationClosingAction(x, _Process);
        //    });
        //}

        public void SubscribeExit(Action<Process> exitAction)
        {
            ExitAction = exitAction;
            _Process.Exited += _Process_Exited;
        }

        public void Kill()
        {
            if(IsRunning())
            {
                KillProcessAndChildren(_Process.Id);
                //_Process.Kill();
                _Process.WaitForExit();
            }
        }

        public bool IsRunning()
        {
            try
            {
                return _Process != null && !_Process.HasExited;
            }
            catch
            {
                return false;
            }
        }

        private void KillProcessAndChildren(int processId)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + processId);
            ManagementObjectCollection managementObjectCollection = searcher.Get();
            foreach (ManagementObject managementObject in managementObjectCollection)
            {
                KillProcessAndChildren(Convert.ToInt32(managementObject["ProcessID"]));
            }
            try
            {
                Process proc = Process.GetProcessById(processId);
                proc.Kill();
            }
            catch (ArgumentException)
            { /* process already exited */ }
        }


    }
}
