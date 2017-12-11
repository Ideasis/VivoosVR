using Core.Common.DataModel.Market;
using Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Client.Models
{
    public class SessionEntryModel
    {
        public Observable<AssetModel> AssetModel { get; set; }
        public Observable<Guid> Id { get; set; }
        public Observable<string> Notes { get; set; }
        public Observable<Guid> ProblemId { get; set; }
        public Observable<string> SensorData { get; set; }

        public Observable<DateTime> SessionDateTime { get; set; }

        public SessionEntryModel()
        {
            AssetModel = new Observable<AssetModel>();
            Notes = new Observable<string>();
            SensorData = new Observable<string>();
            Id = new Observable<Guid>();
            ProblemId = new Observable<Guid>();
            SessionDateTime = new Observable<DateTime>();
        }
    }
}
