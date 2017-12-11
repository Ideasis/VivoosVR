using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MVVM
{
    public interface IBaseViewModel
    {
        IWindsorContainer Container { get; set; }
        Observable<string> FullName { get; set; }
        Observable<string> Title { get; set; }
    }
}
