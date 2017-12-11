using Core.Common.DataModel.Core;
using Core.Log.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Services
{
    [ServiceContract(Namespace = "Core.Server.Services.SessionService.svc")]
    public interface ISessionService
    {
        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        User GetDefaultUser();

        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        User SignIn(string code, string password);
    }
}
