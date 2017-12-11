using Core.Common;
using Core.Common.DataModel;
using Core.Common.DataModel.Market;
using Core.Common.Services;
using Core.Log;
using Core.Log.Exceptions;
using Core.Log.Faults;
using Core.Server.Services;
using Core.Server.Tools;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Market.Server
{

    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    [ErrorBehavior(typeof(CoreErrorHandler))]
    public class PublicMarketService : ServiceBase<VivosEntities>, IPublicMarketService
    {
        public PublicMarketService() : base(new VivosEntities())
        {

        }

        public void DeleteSession(Guid sessionId)
        {
            Session sessionFound = Entities.Sessions.FirstOrDefault(x => x.Id == sessionId);

            if (sessionFound != null)
            {
                Entities.Sessions.Remove(sessionFound);
                Entities.SaveChanges();
            }
        }

        public List<Asset> GetAllAssetsForAdmin()
        {
            string nameOfAssetCommands = "AssetCommands";
            string nameOfAssetDefaults = "AssetDefaults";

            {
                Asset dummyAsset;
                nameOfAssetCommands = nameof(dummyAsset.AssetCommands);
                nameOfAssetDefaults = nameof(dummyAsset.AssetDefaults);
            }


            List<Asset> ret = (from i in Entities.Assets
                          .Include(nameOfAssetCommands)
                          .Include(nameOfAssetDefaults)
                               select i).ToList();

            return ret;
        }

        public Asset GetAsset(Guid id)
        {
            string nameOfAssetCommands = "AssetCommands";
            string nameOfAssetDefaults = "AssetDefaults";
            string nameOfAssetThumbnail = "AssetThumbnail";

            {
                Asset dummyAsset;
                nameOfAssetCommands = nameof(dummyAsset.AssetCommands);
                nameOfAssetDefaults = nameof(dummyAsset.AssetDefaults);
                nameOfAssetThumbnail = nameof(dummyAsset.AssetThumbnail);
            }


            Asset ret = (from i in Entities.Assets
                          .Include(nameOfAssetCommands)
                          .Include(nameOfAssetDefaults)
                          .Include(nameOfAssetThumbnail)
                         where i.Id == id
                         select i).FirstOrDefault();

            return ret;
        }

        public List<AssetCommand> GetAssetCommands(Guid assetId)
        {
            var ret = (from i in Entities.AssetCommands
                       where i.AssetId == assetId
                       select i).OrderBy(x => x.Step).ToList();

            return ret;
        }

        public List<AssetDefault> GetAssetDefaults(Guid assetId)
        {
            var ret = (from i in Entities.AssetDefaults
                       where i.AssetId == assetId
                       select i).OrderBy(x => x.Step).ToList();

            return ret;
        }

        public List<AssetGroup> GetAssetGroups()
        {
            var ret = (from i in Entities.AssetGroups
                            .Include("Assets.Safes")
                       select i).ToList();

            return ret;
        }

        public List<Guid> GetAssetsInSafe()
        {
            List<Guid> ret = new List<Guid>();
            Guid currentUserCompanyId = UserBroker.User.Group.Branch.CompanyId;

            ret = Entities.Safes.Where(x => x.CompanyId == currentUserCompanyId).Select(x => x.AssetId).ToList();

            return ret;
        }

        public AssetThumbnail GetImage(Guid assetId)
        {
            AssetThumbnail ret = (Entities.AssetThumbnails.FirstOrDefault(y => y.AssetId == assetId));

            return ret;
        }

        public List<Patient> GetPatients()
        {
            Guid doctorId = UserBroker.User.Consumer.Id;

            string pathProblems = $"{nameof(Patient.Problems)}";
            return Entities.Patients.Where(x => x.DoctorId == doctorId).Include(pathProblems).ToList();
        }

        public List<Problem> GetProblems(Guid patientId)
        {
            return Entities.Problems.Where(x => x.PatientId == patientId).ToList();
        }

        public Session GetSession(Guid sessionId)
        {
            Session ret = Entities.Sessions.FirstOrDefault(x => x.Id == sessionId);

            return ret;
        }

        public List<Session> GetSessions(Guid problemId)
        {
            List<Session> ret = null;
            ret = Entities
                .Sessions
                .Include("Asset")
                .Where(x => x.ProblemId == problemId)
                .ToList();

            return ret;
        }

        public void SaveAsset(Asset asset)
        {

            string nameOfAssetCommands = "AssetCommands";
            string nameOfAssetDefaults = "AssetDefaults";
            string nameOfAssetThumbnail = "AssetThumbnail";

            {
                Asset dummyAsset;
                nameOfAssetCommands = nameof(dummyAsset.AssetCommands);
                nameOfAssetDefaults = nameof(dummyAsset.AssetDefaults);
                nameOfAssetThumbnail = nameof(dummyAsset.AssetThumbnail);
            }

            Asset assetFound = Entities
                .Assets
                .Include(nameOfAssetCommands)
                .Include(nameOfAssetDefaults)
                .Include(nameOfAssetThumbnail)
                .FirstOrDefault(x => x.Id == asset.Id);

            bool isNew = assetFound == null || asset.Id == Guid.Empty;

            if (isNew)
            {
                asset.Id = Guid.NewGuid();
                asset.EntryDate = DateTime.Now;
                asset.ModifyDate = asset.EntryDate;
                asset.UpdateStamp = Guid.NewGuid();

                Entities.Assets.Add(asset);
                foreach (var item in asset.AssetCommands)
                {
                    item.Id = Guid.NewGuid();
                    Entities.Entry<AssetCommand>(item).State = EntityState.Added;
                }

                foreach (var item in asset.AssetDefaults)
                {
                    item.Id = Guid.NewGuid();
                    Entities.Entry<AssetDefault>(item).State = EntityState.Added;
                }

                Entities.Entry<AssetThumbnail>(asset.AssetThumbnail).State = EntityState.Added;
            }
            else
            {
                Entities.Entry<Asset>(assetFound).CurrentValues.SetValues(asset);

                asset.ModifyDate = DateTime.Now;
                asset.UpdateStamp = Guid.NewGuid();

                foreach (var item in asset.AssetCommands)
                {
                    var assetCommand = assetFound.AssetCommands.FirstOrDefault(x => x.Id == item.Id);
                    if (assetCommand != null)
                    {
                        Entities.Entry<AssetCommand>(assetCommand).CurrentValues.SetValues(item);
                    }
                    else
                    {
                        item.Id = Guid.NewGuid();
                        assetFound.AssetCommands.Add(item);
                    }
                }

                foreach (var item in asset.AssetDefaults)
                {
                    var assetDefault = assetFound.AssetDefaults.FirstOrDefault(x => x.Id == item.Id);
                    if (assetDefault != null)
                    {
                        Entities.Entry<AssetDefault>(assetDefault).CurrentValues.SetValues(item);
                    }
                    else
                    {
                        item.Id = Guid.NewGuid();
                        assetFound.AssetDefaults.Add(item);
                    }
                }

                if (assetFound.AssetThumbnail == null)
                {
                    assetFound.AssetThumbnail = new AssetThumbnail();
                    Entities.Entry<AssetThumbnail>(assetFound.AssetThumbnail).CurrentValues.SetValues(asset.AssetThumbnail);
                }
                else
                {
                    Entities.Entry<AssetThumbnail>(assetFound.AssetThumbnail).CurrentValues.SetValues(asset.AssetThumbnail);
                }
            }

            Entities.SaveChanges();
        }

        public void SavePatient(Patient patient)
        {
            Patient patientFound = Entities.Patients.FirstOrDefault(x => x.Id == patient.Id);
            bool isNew = patientFound == null || patient.Id == Guid.Empty;

            patient.DoctorId = UserBroker.User.Consumer.Id;



            if (!patient.IsApproved)
            {
                throw new ApplicationException("Hastanın anlaşmayı kabul ettiğinden emin olunuz.");
            }

            if (string.IsNullOrWhiteSpace(patient.Code))
            {
                throw new ApplicationException("Hastanın Ad/Kod alanı mutlaka dolu olmalıdır.");
            }

            if (isNew)
            {
                bool codeCheck = Entities.Patients.Any(x => x.Code == patient.Code && x.DoctorId == patient.DoctorId);

                if (codeCheck)
                {
                    throw new ApplicationException($"{patient.Code} olarak belirtilen hasta kodunu daha önce tanımlamışsınız. Lütfen kodu değiştirerek tekrar deneyiniz.");
                }

                patient.Id = Guid.NewGuid();
                patient.EntryDate = DateTime.Now;

                Entities.Patients.Add(patient);
            }
            else
            {
                Entities.Entry<Patient>(patientFound).CurrentValues.SetValues(patient);
            }

            Entities.SaveChanges();
        }

        public void SaveProblem(Problem problem)
        {
            Problem problemFound = Entities.Problems.FirstOrDefault(x => x.Id == problem.Id);
            bool isNew = problemFound == null || problem.Id == Guid.Empty;

            if (string.IsNullOrWhiteSpace(problem.Subject))
            {
                throw new ApplicationException("Şikayet Konusu alanı mutlaka dolu olmalıdır.");
            }

            if (problem.SymptomStartDate > DateTime.Now)
            {
                throw new ApplicationException("Şikayet Başlangıç Tarihi bugünden büyük olamaz.");
            }

            if (problem.IGDPoints < 0)
            {
                throw new ApplicationException("IGD Puanı sıfırdan küçük olamaz.");
            }

            if (isNew)
            {
                problem.Id = Guid.NewGuid();
                problem.EntryDate = DateTime.Now;

                Entities.Problems.Add(problem);
            }
            else
            {
                Entities.Entry<Problem>(problemFound).CurrentValues.SetValues(problem);
            }

            Entities.SaveChanges();
        }

        public void SaveSession(Session session)
        {
            session.SessionDateTime = DateTime.Now;
            Entities.Sessions.Add(session);
            Entities.SaveChanges();
        }
    }
}
