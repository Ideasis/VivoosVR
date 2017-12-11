using Core.Client;
using Core.Common.Converters;
using Core.Common.DataModel.Market;
using Market.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Client.Models
{
    public class AssetAdapter : IAdapter<AssetModel, Asset>, IAdapter<Asset, AssetModel>
    {
        public Asset Convert(AssetModel e)
        {
            Asset ret = new Asset();
            ret.Id = e.Id.Value;
            ret.Name = e.Name.Value;
            ret.Description = e.Description.Value;
            ret.Url = e.Url.Value;
            ret.Exe= e.Exe.Value;
            ret.EntryDate= e.EntryDate.Value;
            ret.UpdateStamp = e.UpdateStamp.Value;
            ret.ModifyDate= e.ModifyDate.Value;

            return ret;
        }

        public AssetModel Convert(Asset entity)
        {
            AssetModel ret = new AssetModel();

            ret.Id.Value = entity.Id;
            ret.Name.Value = entity.Name;
            ret.Description.Value = entity.Description;
            ret.Url.Value = entity.Url;
            ret.Exe.Value = entity.Exe;
            ret.EntryDate.Value = entity.EntryDate;
            ret.UpdateStamp.Value = entity.UpdateStamp;
            ret.ModifyDate.Value = entity.ModifyDate;
            return ret;
        }
    }
}
