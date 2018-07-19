using Core.MVVM;
using Market.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivos.Controls
{
    public interface IBaseObserverControlViewModel : IControlViewModel
    {
        Observable<AssetModel> AssetModel { get; set; }

    }
}
