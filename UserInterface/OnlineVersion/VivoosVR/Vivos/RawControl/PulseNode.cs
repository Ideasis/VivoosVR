using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivos.RawControl
{
    public class PulseNode
    {
        public double Value { get; set; }
        public DateTime Time { get; set; }

        public PulseNode()
        {

        }

        public PulseNode(DateTime time, double value)
        {
            Value = value;
            Time = time;
        }
    }
}
