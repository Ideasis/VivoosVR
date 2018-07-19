using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MVVM
{
    public interface ISecurityChecker
    {
        bool AmIAllowed<T>() where T : IBaseViewModel;

        bool DoIHaveRole(string role);

        string GetCurrentFullNameOfUser();
    }
}
