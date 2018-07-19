using Core.Common;
using Core.Log.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Core.Common.Services
{
    [ServiceContract(Namespace = "Core.Server.Services.OrganizationService.svc")]
    public interface IOrganizationService
    {
        [OperationContract]
        [FaultContract(typeof(CoreFault))]
        string Ping();
    }
}
