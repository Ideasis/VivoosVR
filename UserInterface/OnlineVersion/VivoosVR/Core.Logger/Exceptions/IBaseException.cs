using Core.Log.Faults;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace Core.Log.Exceptions
{
    public interface IBaseException
    {
        string Code { get; set; }
        string Message { get; }

        CoreFault GetFault();
    }
}
