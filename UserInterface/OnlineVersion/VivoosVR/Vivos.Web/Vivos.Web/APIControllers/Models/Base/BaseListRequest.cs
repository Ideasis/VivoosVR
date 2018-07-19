using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VivoosVR.Web.APIControllers.Models.Base
{
    public class BaseListRequest
    {
        public int Current { get; set; }
        public int RowCount { get; set; }
        public string SearchPhrase { get; set; }
        public SortData SortItem { get; set; }
    }
}