using Core.Common.Converters;
using Core.Common.DataModel.Market;
using Core.MVVM;
using Market.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Client.Adapters
{
    public class AssetCommandAdapter : IAdapter<AssetCommandModel, AssetCommand>, IAdapter<AssetCommand, AssetCommandModel>
    {
        public AssetCommand Convert(AssetCommandModel entity)
        {
            AssetCommand ret = new AssetCommand();
            ret.CommandText = entity.CommandText.Value;
            ret.Description = entity.Description.Value;
            ret.Id = entity.Id.Value;
            ret.Step = entity.Step.Value;
            ret.AssetId = entity.AssetId.Value;

            return ret;
        }

        public AssetCommandModel Convert(AssetCommand entity)
        {
            AssetCommandModel ret = new AssetCommandModel()
            {
                CommandText = new Observable<string>(entity.CommandText),
                Description = new Observable<string>(entity.Description),
                Id = new Observable<Guid>(entity.Id),
                Step = new Observable<byte>(entity.Step),
                AssetId = new Observable<Guid>(entity.AssetId)
            };

            return ret;
        }
    }
}
