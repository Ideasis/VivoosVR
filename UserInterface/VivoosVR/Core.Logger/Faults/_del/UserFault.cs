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
    public class UserFault : GenericFault<UserException>
    {
        public UserFault(UserException exc) : base(exc)
        {

        }
    }
}
