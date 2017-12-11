using Core.MVVM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivos.Controls
{
    public interface IPulseControlViewModel : IControlViewModel
    {
        Observable<string> FilePath { get; set; }

        void SetCommand(string command, int step);

        void SetMarker(string marker);

        void SetSwitch(string @switch, int step);

        void SetSud(byte sud);

        void Start();

        void Stop();
    }
}
