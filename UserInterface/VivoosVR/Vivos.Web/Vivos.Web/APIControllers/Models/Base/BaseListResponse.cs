using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VivoosVR.Web.APIControllers.Models.Base
{
    public class BaseListResponse<T> : BaseApiResponse
    {
        public int current { get; set; }

        public int rowCount { get; set; }

        public List<T> rows { get; set; }

        public int total { get; set; }

        public BaseListResponse()
        {
            rows = new List<T>();
        }
    }
}