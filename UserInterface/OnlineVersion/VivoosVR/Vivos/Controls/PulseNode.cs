using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivos.Controls
{
    public class PulseNode
    {
        public double Pulse { get; set; }
        public double GSR { get; set; }
        public byte SUD { get; set; }
        public DateTime Time { get; set; }
        public string Marker { get; set; }
        public string CommandText { get; set; }
        public string SwitchText { get; set; }
        public int CommandStep { get; set; }
        public int SwitchStep { get; set; }

        public PulseNode()
        {

        }

        public PulseNode(DateTime time, double gsr, double pulse, string marker, string command, int commandStep, string @switch, int switchStep, byte sud)
        {
            GSR = gsr;
            Pulse = pulse;
            Time = time;
            Marker = marker;
            CommandText = command;
            SwitchText = @switch;
            SUD = sud;
            CommandStep = commandStep;
            SwitchStep = switchStep;
        }

        public override string ToString()
        {
            return $"{DateTime.Now.ToString("hh:mm:ss.fff")},{Pulse.ToString(CultureInfo.GetCultureInfo("en-US"))},{GSR.ToString(CultureInfo.GetCultureInfo("en-US"))},{Marker},{CommandText},{CommandStep},{SwitchText},{SwitchStep},{SUD}";
        }
    }
}
