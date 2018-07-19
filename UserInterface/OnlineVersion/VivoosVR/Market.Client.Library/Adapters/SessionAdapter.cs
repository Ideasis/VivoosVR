using Core.Common.Converters;
using Core.MVVM;
using Market.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.DataModel.Market;

namespace Market.Client.Adapters
{
    public class SessionAdapter : IAdapter<SessionEntryModel, Session>, IAdapter<Session, SessionEntryModel>
    {
        public Session Convert(SessionEntryModel e)
        {
            AssetAdapter assetAdapter = new AssetAdapter();

            Session ret = new Session
            {
                AssetId = e.AssetModel.Value.Id.Value,
                Id = e.Id.Value,
                Notes = e.Notes.Value,
                ProblemId = e.ProblemId.Value,
                SensorData = e.SensorData.Value,
                SessionDateTime = e.SessionDateTime.Value,
                Asset = assetAdapter.Convert(e.AssetModel.Value)
            };

            return ret;
        }

        public SessionEntryModel Convert(Session e)
        {
            SessionEntryModel ret = new SessionEntryModel();
            AssetAdapter assetAdapter = new AssetAdapter();

            ret.Id.Value = e.Id;
            ret.Notes.Value = e.Notes;
            ret.ProblemId.Value = e.ProblemId;
            ret.SensorData.Value = e.SensorData;
            ret.SessionDateTime.Value = e.SessionDateTime;
            ret.AssetModel.Value = assetAdapter.Convert(e.Asset);

            return ret;
        }
    }
}
