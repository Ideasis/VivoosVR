using Core.MVVM;
using Market.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivos.Controls
{
    public interface IObserverControlViewModel : IControlViewModel
    {
        Observable<AssetModel> Asset { get; set; }
    }
}
