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
    public class PatientAdapter : IAdapter<PatientEntryModel, Patient>, IAdapter<Patient, PatientEntryModel>
    {
        public Patient Convert(PatientEntryModel e)
        {
            Patient ret = new Patient();
            ret.Id = e.Id.Value;
            ret.IsApproved = e.IsApproved.Value;
            ret.DateOfBirth = e.DateOfBirth.Value;
            ret.Code = e.Code.Value;
            ret.DoctorId = e.DoctorId.Value;
            ret.EntryDate = e.EntryDate.Value;


            return ret;
        }

        public PatientEntryModel Convert(Patient e)
        {
            PatientEntryModel ret = new PatientEntryModel();
            ret.DateOfBirth.Value = e.DateOfBirth;
            ret.Code.Value = e.Code;
            ret.DoctorId.Value = e.DoctorId;
            ret.Id.Value = e.Id;
            ret.IsApproved.Value = e.IsApproved;
            ret.EntryDate.Value = e.EntryDate;

            return ret;
        }
    }
}
