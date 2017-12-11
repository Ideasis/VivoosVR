using Core.Common.Converters;
using Core.Common.DataModel;
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
    public class AssetDefaultAdapter : IAdapter<AssetDefaultModel, AssetDefault>, IAdapter<AssetDefault, AssetDefaultModel>
    {
        public AssetDefault Convert(AssetDefaultModel e)
        {
            AssetDefault ret = new AssetDefault();
            ret.Description = e.Description.Value;
            ret.Id = e.Id.Value;
            ret.OffCommandName = e.OffCommandName.Value;
            ret.OffCommandText = e.OffCommandText.Value;
            ret.OnCommandName = e.OnCommandName.Value;
            ret.OnCommandText = e.OnCommandText.Value;
            ret.Step = e.Step.Value;
            ret.AssetId = e.AssetId.Value;
            ret.DefaultValue = e.DefaultValue.Value;

            return ret;
        }

        public AssetDefaultModel Convert(AssetDefault e)
        {
            AssetDefaultModel ret = new AssetDefaultModel()
            {
                OnCommandText = new Observable<string>(e.OnCommandText),
                OffCommandText = new Observable<string>(e.OffCommandText),
                OnCommandName = new Observable<string>(e.OnCommandName),
                OffCommandName = new Observable<string>(e.OffCommandName),
                Description = new Observable<string>(e.Description),
                Id = new Observable<Guid>(e.Id),
                Step = new Observable<byte>(e.Step),
                AssetId = new Observable<Guid>(e.AssetId),
                DefaultValue = new Observable<bool>(e.DefaultValue)
            };

            return ret;
        }
    }
}
