using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VivoosVR.Web.APIControllers.Models.Base;

namespace VivoosVR.Web.APIControllers.Models
{
    public class GetAllAssetsWithImagesResponse : MessageResponse
    {
        public List<AssetWithImage> Assets { get; set; }

        public GetAllAssetsWithImagesResponse()
        {
            Assets = new List<AssetWithImage>();
        }
    }
}