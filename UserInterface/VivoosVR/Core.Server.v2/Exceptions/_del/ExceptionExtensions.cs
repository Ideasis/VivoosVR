using Core.Log.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Exceptions
{
    public static class ExceptionExtensions
    {
        public static FaultException GetFault(this Exception exc)
        {
            return new FaultException<Exception>(exc, new FaultReason(exc.Message));
        }

        //public static FaultException<T> GetBaseFault<T>(this T exc) where T : BaseException
        //{
        //    return new FaultException<T>(exc, new FaultReason(exc.Message));
        //}
    }
}
