using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VivoosVR.Web.APIControllers.Models
{
    public class AssetWithImage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}