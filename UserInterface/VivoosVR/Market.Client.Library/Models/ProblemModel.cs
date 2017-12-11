using Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Client.Models
{
    public class ProblemModel
    {
        public Observable<string> Avoidance { get; set; }
        public Observable<string> History { get; set; }
        public Observable<Guid> Id { get; set; }
        public Observable<int> IGDPoints { get; set; }
        public Observable<Guid?> PatientId { get; set; }
        public Observable<string> Precaution { get; set; }
        public Observable<string> Subject { get; set; }
        public Observable<string> SymptomDescription { get; set; }
        public Observable<int> SymptomDurationInDays { get; set; }
        public Observable<string> SymptomResults { get; set; }
        public Observable<string> SymptomType { get; set; }

        public ProblemModel()
        {
            PatientId = new Observable<Guid?>();
            Subject = new Observable<string>();
            SymptomDescription = new Observable<string>();
            SymptomType = new Observable<string>();
            SymptomDurationInDays = new Observable<int>();
            SymptomResults = new Observable<string>();
            IGDPoints = new Observable<int>();
            Avoidance = new Observable<string>();
            Precaution = new Observable<string>();
            History = new Observable<string>();

            PatientId.SubscribeValidate(x =>
            {
                if (x == null || x == Guid.Empty)
                {
                    return "Hasta bilgisi mutlaka seçilmelidir.";
                }

                return null;
            });

            PatientId.SubscribeValidate(x =>
            {
                if (x == null || x == Guid.Empty)
                {
                    return "Hasta bilgisi mutlaka seçilmelidir.";
                }

                return null;
            });

            Subject.SubscribeValidate(x =>
            {
                if (string.IsNullOrWhiteSpace(x))
                {
                    return "Konu alanı mutlaka doldurulmalıdır.";
                }

                return null;
            });

            SymptomDescription.SubscribeValidate(x =>
                {
                    if (string.IsNullOrWhiteSpace(x))
                    {
                        return "Belirtiler alanı mutlaka dolu olmalıdır.";
                    }

                    return null;
                });

            SymptomType.SubscribeValidate(x =>
                {
                    if (string.IsNullOrWhiteSpace(x))
                    {
                        return "Belirti Tipi alanı mutlaka dolu olmalıdır.";
                    }

                    return null;
                });

            SymptomDurationInDays.SubscribeValidate(x =>
                {
                    if (x <= 0)
                    {
                        return "Belirti süresi alanı mutlaka dolu olmalıdır.";
                    }
                    return null;
                });

            SymptomResults.SubscribeValidate(x =>
                {
                    if (string.IsNullOrWhiteSpace(x))
                    {
                        return "Belirti Sonuçları alanı mutlaka dolu olmalıdır.";
                    }

                    return null;
                });

            IGDPoints.SubscribeValidate(x =>
                {
                    return null;
                });

            Avoidance.SubscribeValidate(x =>
                {
                    if (string.IsNullOrWhiteSpace(x))
                    {
                        return "Sakınmalar alanı mutlaka dolu olmalıdır.";
                    }

                    return null;
                });

            Precaution.SubscribeValidate(x =>
                {
                    if (string.IsNullOrWhiteSpace(x))
                    {
                        return "Tedbirler alanı mutlaka dolu olmalıdır.";
                    }

                    return null;
                });

            History.SubscribeValidate(x =>
                {
                    if (string.IsNullOrWhiteSpace(x))
                    {
                        return "Geçmiş alanı mutlaka dolu olmalıdır.";
                    }

                    return null;
                });


        }
    }
}
