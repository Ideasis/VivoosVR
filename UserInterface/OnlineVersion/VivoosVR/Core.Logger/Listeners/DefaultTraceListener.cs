using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Log.Listeners
{
    public class DefaultTraceListener : ConsoleTraceListener
    {
        public DefaultTraceListener()
        {
            Debug.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Debug.WriteLine(string.Format("Initializing DefaultTraceListener. ({0})", DateTime.Now));
            Debug.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        }

        public override void Write(string message)
        {
            Debug.Write(message);
        }

        public override void WriteLine(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
