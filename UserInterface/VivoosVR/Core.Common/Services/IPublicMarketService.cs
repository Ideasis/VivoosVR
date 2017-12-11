using Core.Common.DataModel.Market;
using Core.Log.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Services
{
    [ServiceContract(Namespace = "Market.Server.PublicMarketService.svc")]
    public interface IPublicMarketService
    {
        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        List<Asset> GetAllAssetsForAdmin();

        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        Asset GetAsset(Guid id);

        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        List<AssetCommand> GetAssetCommands(Guid assetId);

        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        List<AssetDefault> GetAssetDefaults(Guid assetId);

        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        List<AssetGroup> GetAssetGroups();

        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        AssetThumbnail GetImage(Guid assetId);

        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        List<Patient> GetPatients();

        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        List<Problem> GetProblems(Guid patientId);

        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        void SaveAsset(Asset asset);

        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        void SavePatient(Patient patient);

        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        void SaveProblem(Problem problem);


        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        List<Guid> GetAssetsInSafe();

        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        List<Session> GetSessions(Guid problemId);

        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        void SaveSession(Session session);

        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        void DeleteSession(Guid sessionId);

        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        Session GetSession(Guid sessionId);
    }
}
