using Core.MVVM;
using DevExpress.Xpf.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivos.Controls
{
    public interface IPulseControl : IControl
    {
        ChartControl Chart { get; }
        //LineSeries2D GSRSeries { get; }
        //LineSeries2D PulseSeries { get; }
        
    }
}
