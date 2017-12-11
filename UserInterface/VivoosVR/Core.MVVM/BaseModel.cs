using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MVVM
{
    public abstract class BaseModel : ReactiveObject, IModel, IDataErrorInfo
    {
        public abstract string Error { get; }
        public abstract string this[string columnName] { get; }

        public abstract void Reset();
    }
}
