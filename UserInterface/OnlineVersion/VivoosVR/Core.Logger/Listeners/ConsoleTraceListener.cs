using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Log.Listeners
{
    public class ConsoleTraceListener : System.Diagnostics.ConsoleTraceListener
    {
        public ConsoleTraceListener()
        {
            Debug.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            Debug.WriteLine(string.Format("Initializing ConsoleTraceListener. ({0})", DateTime.Now));
            Debug.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        }

        public override void Write(string message)
        {
            Console.Write(message);
        }

        public override void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
