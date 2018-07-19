using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Log
{
    public interface IClaim
    {
        string SecurityCode { get; set; }
    }
}
