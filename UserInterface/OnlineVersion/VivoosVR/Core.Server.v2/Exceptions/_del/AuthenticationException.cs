using Core.Log.Exceptions;
using Core.Log.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Exceptions
{
    public class AuthenticationException : BaseException, IBaseException
    {
        public AuthenticationException(string code, string message, params object[] args) : base(code, message, args)
        {
        }

        public CoreFault GetFault()
        {
            return new CoreFault(this, Message);
        }
    }
}
