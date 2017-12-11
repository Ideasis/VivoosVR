using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MVVM
{
    public struct Arg
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public Arg(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return $"[{Name} : {Value.GetType().FullName}]";
        }
    }
}
