using Core.Log.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Log.Exceptions
{
    [Serializable]
    public class UserException : BaseException, IBaseException
    {
        public UserException(string message, params object[] args) : base(message, args)
        {
        }

        public UserException(string code, string message, params object[] args) : base(code, message, args)
        {
        }

        public CoreFault GetFault()
        {
            return new CoreFault(this, Message);
        }
    }
}
