using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Constants
{
    public enum AuthenticationStatus
    {
        Succeed,
        DefaultUser,
        UnknownUser,
        Expired
    }
}
