using Core.Log.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Exceptions
{
    public class TupleException : BaseException
    {
        public TupleException(string message, params object[] args)
                    : base(message, args)
        {

        }
    }
}
