using Core.Common.Converters;
using Core.Common.DataModel;
using Core.Common.DataModel.Market;
using Market.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Client.Adapters
{
    public class AssetGroupAdapter : IAdapter<AssetGroupModel, AssetGroup>
    {
        public AssetGroupModel Convert(AssetGroup e)
        {
            AssetGroupModel ret = new AssetGroupModel();

            ret.Name.Value = e.Name;
            ret.Description.Value = e.Description;
            ret.Id.Value = e.Id;

            AssetAdapter adapter = new AssetAdapter();

            e.Assets.ToList().ForEach(x =>
            {
                ret.Assets.Value.Add(adapter.Convert(x));
            });

            return ret;
        }
    }
}
