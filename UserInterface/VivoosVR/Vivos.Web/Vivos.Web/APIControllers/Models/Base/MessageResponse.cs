using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VivoosVR.Web.APIControllers.Models.Base
{
    public class MessageResponse
    {
        public string Message { get; set; } = null;
        public bool HasError { get; set; } = false;
    }
}