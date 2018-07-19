using VivoosVR.Web.APIControllers.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VivoosVR.Web.APIControllers.Models
{
    public class KeyValueResponse : MessageResponse
    {
        public List<KeyValueResponseItem> Items { get; set; }
    }

    public class KeyValueResponseItem
    {
        public decimal Key { get; set; }
        public string Value { get; set; }
    }
}