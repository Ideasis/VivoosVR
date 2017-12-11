using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VivoosVR.Web.APIControllers.Models.Base
{
    public class SortData
    {
        public string Field { get; set; } // FIeld Name
        public string Type { get; set; } // ASC or DESC
        public Func<string, string> SortFunc { get; set; }

        public override string ToString()
        {
            if (SortFunc == null) SortFunc = x => { return x; };
            return $"{SortFunc(Field)} {Type}";
        }
    }
}