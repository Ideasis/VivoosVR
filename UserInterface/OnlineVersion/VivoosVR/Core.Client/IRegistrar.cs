using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Client
{
    public interface IRegistrar
    {
        IWindsorContainer Container { get; set; }
    }
}
