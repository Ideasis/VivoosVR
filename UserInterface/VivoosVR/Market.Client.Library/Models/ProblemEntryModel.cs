using Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Client.Models
{
    public class ProblemEntryModel
    {
        public Observable<string> Avoidance { get; set; }
        public Observable<DateTime> EntryDate { get; set; }
        public Observable<string> History { get; set; }
        public Observable<Guid> Id { get; set; }
        public Observable<int?> IGDPoints { get; set; }
        public Observable<Guid> PatientId { get; set; }
        public Observable<string> PlaceOfFullSecure { get; set; }
        public Observable<string> PlaceOfFullUnsecure { get; set; }
        public Observable<string> PlaceOfHesitant { get; set; }
        public Observable<string> PlaceOfSecure { get; set; }
        public Observable<string> PlaceOfUnsecure { get; set; }
        public Observable<string> Precaution { get; set; }
        public Observable<string> Subject { get; set; }
        public Observable<string> SymptomDescription { get; set; }
        public string SymptomDescriptionShorten
        {
            get
            {
                if (SymptomDescription == null || SymptomDescription.Value == null) return "";

                return SymptomDescription.Value.Length > 200 ? SymptomDescription.Value.Substring(0, 200) + "..." : SymptomDescription.Value;
            }
        }
        public Observable<string> SymptomResults { get; set; }
        public Observable<DateTime?> SymptomStartDate { get; set; }
        public Observable<string> SymptomType { get; set; }

        public ProblemEntryModel()
        {
            Id = new Observable<Guid>();
            PatientId = new Observable<Guid>();
            Subject = new Observable<string>();
            SymptomDescription = new Observable<string>();
            SymptomType = new Observable<string>();
            SymptomStartDate = new Observable<DateTime?>(DateTime.Today);
            SymptomResults = new Observable<string>();
            IGDPoints = new Observable<int?>();
            Avoidance = new Observable<string>();
            Precaution = new Observable<string>();
            History = new Observable<string>();
            EntryDate = new Observable<DateTime>();

            PlaceOfFullSecure = new Observable<string>();
            PlaceOfSecure = new Observable<string>();
            PlaceOfHesitant = new Observable<string>();
            PlaceOfUnsecure = new Observable<string>();
            PlaceOfFullUnsecure = new Observable<string>();


            Id.SubscribeValidate(x =>
                {
                    if (x == Guid.Empty)
                    {
                        return "Id alanı mutlaka dolu olmalıdır.";
                    }
                    return null;
                });

            PatientId.SubscribeValidate(x =>
            {
                if (x == Guid.Empty)
                {
                    return "Hasta seçimi yapılmamış.";
                }
                return null;
            });

            Subject.SubscribeValidate(x =>
                {
                    if (string.IsNullOrWhiteSpace(x))
                    {
                        return "Konu alanı mutlaka dolu olmalıdır.";
                    }
                    if (x.Length > 200)
                    {
                        return "Bu alana en fazla 200 karakter olabilir.";
                    }

                    return null;
                });

            //SymptomDescription.SubscribeValidate(x =>
            //    {
            //        if (string.IsNullOrWhiteSpace(x))
            //        {
            //            return "Belirtiler alanı mutlaka dolu olmalıdır.";
            //        }

            //        return null;
            //    });

            //SymptomType.SubscribeValidate(x =>
            //    {
            //        if (string.IsNullOrWhiteSpace(x))
            //        {
            //            return "Belirti türü mutlaka dolu olmalıdır.";
            //        }

            //        return null;
            //    });

            //SymptomResults.SubscribeValidate(x =>
            //    {
            //        if (string.IsNullOrWhiteSpace(x))
            //        {
            //            return "Belirti sonuçları mutlaka dolu olmalıdır.";
            //        }

            //        return null;
            //    });

            //IGDPoints.SubscribeValidate(x =>
            //    {
            //        if (x == 0)
            //        {
            //            return "IGD puanlaması mutlaka dolu olmalıdır.";
            //        }
            //        return null;
            //    });

            PlaceOfFullSecure.SubscribeValidate(x =>
            {
                if (x != null && x.Length > 200)
                {
                    return "Bu alana en fazla 200 karakter olabilir.";
                }
                return null;
            });

            PlaceOfSecure.SubscribeValidate(x =>
            {
                if (x != null && x.Length > 200)
                {
                    return "Bu alan en fazla 200 karakter olabilir.";
                }
                return null;
            });

            PlaceOfHesitant.SubscribeValidate(x =>
            {
                if (x != null && x.Length > 200)
                {
                    return "Bu alan en fazla 200 karakter olabilir.";
                }
                return null;
            });

            PlaceOfUnsecure.SubscribeValidate(x =>
            {
                if (x != null && x.Length > 200)
                {
                    return "Bu alan en fazla 200 karakter olabilir.";
                }
                return null;
            });

            PlaceOfFullUnsecure.SubscribeValidate(x =>
            {
                if (x != null && x.Length > 200)
                {
                    return "Bu alan en fazla 200 karakter olabilir.";
                }
                return null;
            });
        }
    }
}
