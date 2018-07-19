using Core.MVVM;
using Market.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivos.Controls
{
    public interface IAssetEntryControlViewModel : IControlViewModel
    {
        Observable<AssetEntryModel> AssetEntryModel { get; set; }
    }
}
