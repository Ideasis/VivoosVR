using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Server.Helpers
{
    public static class Translator
    {
        public static string Translate(this string text, params object[] args)
        {
            return string.Format(text, args);
        }
    }
}
