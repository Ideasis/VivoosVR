using Castle.Windsor;
using Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivos.Controls
{
    public class SecurityFailedControlViewModel : BaseControlViewModel, ISecurityFailedControlViewModel
    {
        public SecurityFailedControlViewModel(IWindsorContainer container, ISecurityFailedControl view)
                    : base(container, view, "Güvenlik Hatası")
        {

        }
    }
}
