using Core.Log.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Core.Log.Faults
{
    [DataContract]
    public class CoreFault 
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string UserMessage { get; set; }

        public CoreFault(IBaseException exc, string userMessage = null) 
        {
            Code = exc.Code;
            Message = exc.Message;
            UserMessage = userMessage;
        }

        public CoreFault(ApplicationException exc, string userMessage = null)
        {
            Message = exc.Message;
            UserMessage = userMessage;
        }
    }
}
