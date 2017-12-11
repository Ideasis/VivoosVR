using Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Client.Models
{
    public class AssetDefaultModel
    {
        public Observable<Guid> AssetId { get; set; }
        public Observable<string> CurrentName { get; set; }
        public Observable<bool> DefaultValue { get; set; }
        public Observable<string> Description { get; set; }
        public Observable<Guid> Id { get; set; }
        public Observable<bool> IsValid { get; set; }
        public Observable<string> OffCommandName { get; set; }
        public Observable<string> OffCommandText { get; set; }
        public Observable<string> OnCommandName { get; set; }
        public Observable<string> OnCommandText { get; set; }
        public Observable<bool> OnOffSwitch { get; set; }
        public Observable<byte> Step { get; set; }

        public AssetDefaultModel()
        {
            OnOffSwitch = new Observable<bool>(false);
            CurrentName = new Observable<string>("");
            OnCommandText = new Observable<string>();
            OnCommandName = new Observable<string>();
            OffCommandText = new Observable<string>();
            OffCommandName = new Observable<string>();
            Description = new Observable<string>();
            Step = new Observable<byte>();
            Id = new Observable<Guid>();
            IsValid = new Observable<bool>(true);
            AssetId = new Observable<Guid>();
            DefaultValue = new Observable<bool>(false);

            OnOffSwitch.Subscribe(x =>
            {
                CurrentName.Value = x ? OnCommandName.Value : OffCommandName.Value;
            });
            OnCommandText.SubscribeValidate(x =>
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    IsValid.Value = false;
                    return "Bu alan mutlaka dolu olmalıdır.";
                }
                return null;
            });
            OnCommandName.SubscribeValidate(x =>
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    IsValid.Value = false;
                    return "Bu alan mutlaka dolu olmalıdır.";
                }
                return null;
            });
            OffCommandText.SubscribeValidate(x =>
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    IsValid.Value = false;
                    return "Bu alan mutlaka dolu olmalıdır.";
                }
                return null;
            });
            OffCommandName.SubscribeValidate(x =>
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    IsValid.Value = false;
                    return "Bu alan mutlaka dolu olmalıdır.";
                }
                return null;
            });

            Description.SubscribeValidate(x =>
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    IsValid.Value = false;
                    return "Bu alan mutlaka dolu olmalıdır.";
                }
                return null;
            });
        }
    }
}
