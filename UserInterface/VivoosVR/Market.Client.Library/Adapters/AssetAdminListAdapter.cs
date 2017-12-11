using Core.Common.Converters;
using Core.Common.DataModel.Market;
using Market.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Client.Adapters
{
    public class AssetAdminListAdapter : IAdapter<AssetAdminListModel, Asset>
    {
        public AssetAdminListModel Convert(Asset e)
        {
            AssetAdminListModel ret = new AssetAdminListModel();
            ret.Available.Value = e.Available;
            ret.Description.Value = e.Description;
            ret.ExeName.Value = e.Exe;
            ret.Id.Value = e.Id;
            ret.Name.Value = e.Name;
            ret.Url.Value = e.Url;
            ret.ModifyDate.Value = e.ModifyDate;

            return ret;
        }
    }
}
