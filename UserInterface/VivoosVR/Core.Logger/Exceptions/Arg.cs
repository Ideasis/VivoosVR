using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Log.Exceptions
{
    public class Arg : Tuple<string, object>
    {
        public Arg(string name, object value)
            : base(name, value)
        {
        }


        public string Name
        {
            get { return Item1; }
        }

        public object Value
        {
            get { return Item2; }
        }


        public string ToDumpString()
        {
            return string.Format("{0} : {1}", Name, Value);
        }

        public override string ToString()
        {
            //return string.Format("{0}:{1}", Name, Value);
            return Value.ToString();
        }

    }
}