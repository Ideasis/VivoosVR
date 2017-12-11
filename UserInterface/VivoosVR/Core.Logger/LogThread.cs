using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Log
{
    public class LogThread
    {
        public Stopwatch Timing { get; set; }
        public StringBuilder Message { get; set; }
        public string Header { get; set; }

        public LogThread(string header)
        {
            Header = header;

            Timing = new Stopwatch();
            Timing.Start();

            Message = new StringBuilder();
            Message.AppendLine($"+ {header} ---------------------------------");
        }

        public void Append(string message)
        {
            Message.Append($"Time: {Timing.Elapsed}: {message}");
        }

        public void AppendLine(string message)
        {
            Message.AppendLine($"Time: {Timing.Elapsed}: {message}");
        }

        public override string ToString()
        {
            Message.AppendLine($"- {Header} ---------------------------------");

            return Message.ToString();
        }
    }
}
