using Core.Log.Exceptions;
using Core.Log.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Log.Exceptions
{
    [Serializable]
    public class SecurityException : BaseException, IBaseException
    {
        const string _MessageFormat = @"{0} olarak belirtilen kaynağa erişiminiz engellendi.";

        public string Claim { get; set; }

        public SecurityException(string claim, string message) : base(claim, string.Format(_MessageFormat, claim))
        {
        }

        public static SecurityException CreateCoreException<TClaim>(TClaim claim) where TClaim : IClaim
        {
            SecurityException ret = new SecurityException(claim.SecurityCode, string.Format(_MessageFormat, claim.SecurityCode));
            return ret;
        }

        public CoreFault GetFault()
        {
            return new CoreFault(this, Message);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
