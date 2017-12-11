using Core.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Client.Models
{
    public class PatientEntryModel
    {
        public Observable<DateTime?> DateOfBirth { get; set; }
        public Observable<string> Code { get; set; }
        public Observable<Guid> DoctorId { get; set; }
        public Observable<DateTime> EntryDate { get; set; }
        public Observable<Guid> Id { get; set; }
        public Observable<bool> IsApproved { get; set; }

        public PatientEntryModel()
        {
            Code = new Observable<string>();
            DateOfBirth = new Observable<DateTime?>();
            IsApproved = new Observable<bool>(false);
            DoctorId = new Observable<Guid>();
            Id = new Observable<Guid>();
            EntryDate = new Observable<DateTime>();

            Code.SubscribeValidate(x =>
                {
                    if (string.IsNullOrWhiteSpace(x))
                    {
                        return "Kod alanı mutlaka dolu olmalıdır.";
                    }

                    return null;
                });

            DoctorId.SubscribeValidate(x =>
                {
                    if (x == Guid.Empty)
                    {
                        return "Doktor alanı mutlaka seçili olmalıdır.";
                    }
                    return null;
                });

            IsApproved.SubscribeValidate(x =>
                {
                    if (!x)
                    {
                        return "Hastanın anlaşmayı onayladığından emin olunuz.";
                    }
                    return null;
                });
        }
    }
}
