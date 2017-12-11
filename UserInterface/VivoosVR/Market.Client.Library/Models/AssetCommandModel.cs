using Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Client.Models
{
    public class AssetCommandModel
    {
        public Observable<Guid> AssetId { get; set; }
        public Observable<string> CommandText { get; set; }
        public Observable<string> Description { get; set; }
        public Observable<Guid> Id { get; set; }
        public Observable<byte> Step { get; set; }

        public AssetCommandModel()
        {
            Id = new Observable<Guid>();
            CommandText = new Observable<string>();
            Description = new Observable<string>();
            Step = new Observable<byte>();
            AssetId = new Observable<Guid>();

            CommandText.SubscribeValidate(x =>
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    return "Bu alan mutlaka dolu olmalıdır.";
                }
                return null;
            });

            Description.SubscribeValidate(x =>
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    return "Bu alan mutlaka dolu olmalıdır.";
                }
                return null;
            });
        }
    }
}
