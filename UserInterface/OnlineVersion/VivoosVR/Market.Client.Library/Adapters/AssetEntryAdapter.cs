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
    public class AssetEntryAdapter : IAdapter<Asset, AssetEntryModel>, IAdapter<AssetEntryModel, Asset>
    {
        public AssetEntryModel Convert(Asset e)
        {
            AssetEntryModel ret = new AssetEntryModel();
            ret.GroupId.Value = e.GroupId;
            ret.Id.Value = e.Id;
            ret.Image.Value = e.AssetThumbnail == null ? null : e.AssetThumbnail.Thumbnail;
            ret.ImagePath.Value = "";
            ret.Name.Value = e.Name;
            ret.Url.Value = e.Url;
            ret.ZipPath.Value = "";
            ret.Available.Value = e.Available;
            ret.Description.Value = e.Description;
            ret.Exe.Value = e.Exe;
            ret.EntryDate.Value = e.EntryDate;
            ret.ModifyDate.Value = e.ModifyDate;


            AssetCommandAdapter commandAdapter = new AssetCommandAdapter();
            AssetDefaultAdapter defaultAdapter = new AssetDefaultAdapter();

            foreach (var item in e.AssetCommands)
            {
                ret.Commands.Value.Add(commandAdapter.Convert(item));
            }

            foreach (var item in e.AssetDefaults)
            {
                ret.Defaults.Value.Add(defaultAdapter.Convert(item));
            }

            return ret;
        }

        public Asset Convert(AssetEntryModel e)
        {
            AssetCommandAdapter commandAdapter = new AssetCommandAdapter();
            AssetDefaultAdapter defaultAdapter = new AssetDefaultAdapter();
            Asset ret = new Asset();

            ret.Id = e.Id.Value;
            ret.Available = e.Available.Value;
            ret.Description = e.Description.Value;
            ret.Exe = e.Exe.Value;
            ret.GroupId = e.GroupId.Value;
            ret.Name = e.Name.Value;
            ret.Url = e.Url.Value;
            ret.EntryDate = e.EntryDate.Value;
            ret.ModifyDate = e.ModifyDate.Value;

            if (ret.AssetThumbnail == null) ret.AssetThumbnail = new AssetThumbnail();
            ret.AssetThumbnail.Asset = ret;
            ret.AssetThumbnail.AssetId = ret.Id;
            ret.AssetThumbnail.Thumbnail = e.Image.Value;

            e.Defaults.Value.ToList().ForEach(x =>
            {
                ret.AssetDefaults.Add(defaultAdapter.Convert(x));
            });

            e.Commands.Value.ToList().ForEach(x =>
            {
                ret.AssetCommands.Add(commandAdapter.Convert(x));
            });



            return ret;
        }
    }
}
