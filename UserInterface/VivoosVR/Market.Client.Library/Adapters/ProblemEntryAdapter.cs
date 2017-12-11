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
    public class ProblemEntryAdapter : IAdapter<ProblemEntryModel, Problem>, IAdapter<Problem, ProblemEntryModel>
    {
        public Problem Convert(ProblemEntryModel e)
        {
            Problem ret = new Problem();
            ret.Avoidance = e.Avoidance.Value;
            ret.History = e.History.Value;
            ret.Id = e.Id.Value;
            ret.IGDPoints = e.IGDPoints.Value;
            ret.PatientId = e.PatientId.Value;
            ret.Precaution = e.Precaution.Value;
            ret.Subject = e.Subject.Value;
            ret.SymptomDescription = e.SymptomDescription.Value;
            ret.SymptomStartDate = e.SymptomStartDate.Value;
            ret.SymptomResults = e.SymptomResults.Value;
            ret.SymptomType = e.SymptomType.Value;
            ret.PlaceOfFullSecure = e.PlaceOfFullSecure.Value;
            ret.PlaceOfSecure = e.PlaceOfSecure.Value;
            ret.PlaceOfHesitant = e.PlaceOfHesitant.Value;
            ret.PlaceOfUnsecure = e.PlaceOfUnsecure.Value;
            ret.PlaceOfFullUnsecure = e.PlaceOfFullUnsecure.Value;
            ret.EntryDate = e.EntryDate.Value;

            return ret;
        }

        public ProblemEntryModel Convert(Problem e)
        {
            ProblemEntryModel ret = new ProblemEntryModel();
            ret.Avoidance.Value = e.Avoidance;
            ret.History.Value = e.History;
            ret.Id.Value = e.Id;
            ret.IGDPoints.Value = e.IGDPoints;
            ret.PatientId.Value = e.PatientId;
            ret.Precaution.Value = e.Precaution;
            ret.Subject.Value = e.Subject;
            ret.SymptomDescription.Value = e.SymptomDescription;
            ret.SymptomStartDate.Value = e.SymptomStartDate;
            ret.SymptomResults.Value = e.SymptomResults;
            ret.SymptomType.Value = e.SymptomType;
            ret.PlaceOfFullSecure.Value = e.PlaceOfFullSecure;
            ret.PlaceOfSecure.Value = e.PlaceOfSecure;
            ret.PlaceOfHesitant.Value = e.PlaceOfHesitant;
            ret.PlaceOfUnsecure.Value = e.PlaceOfUnsecure;
            ret.PlaceOfFullUnsecure.Value = e.PlaceOfFullUnsecure;
            ret.EntryDate.Value = e.EntryDate;

            return ret;
        }
    }
}
