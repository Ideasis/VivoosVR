using VivoosVR.Web.APIControllers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Linq.Dynamic;
using VivoosVR.Web.APIControllers.Models.Base;
using System.Threading;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using Core.Log;

namespace VivoosVR.Web.APIControllers
{
    [Authorize]
    [RoutePrefix("api/customer")]
    public class CustomerApiController : BaseApiController
    {

        [HttpGet]
        [Route("getmyassetswithimages")]
        public GetAllAssetsWithImagesResponse GetMyAssetsWithImages(BaseListRequest r)
        {
            GetAllAssetsWithImagesResponse ret = new GetAllAssetsWithImagesResponse();

            try
            {
                var user = GetCurrentUser();
                var userCompanyId = user.User.Group.Branch.CompanyId;

                var assets = VivosEntities.Safes.Where(x => x.CompanyId == userCompanyId).Select(x => x.Asset);
                foreach (var asset in assets)
                {
                    ret.Assets.Add(new AssetWithImage()
                    {
                        Id = asset.Id,
                        Description = asset.Description,
                        Image = Convert.ToBase64String(asset.AssetThumbnail.Thumbnail),
                        Name = asset.Name
                    });
                }

                ret.Message = "Operation succeed.";
                ret.HasError = false;
            }
            catch (Exception exc)
            {
                exc.LogException();
                throw;
            }

            return ret;
        }

        [HttpGet]
        [Route("getnotmyassetswithimages")]
        public GetAllAssetsWithImagesResponse GetNotMyAssetsWithImages(BaseListRequest r)
        {
            GetAllAssetsWithImagesResponse ret = new GetAllAssetsWithImagesResponse();

            try
            {
                var user = GetCurrentUser();
                var userCompanyId = user.User.Group.Branch.CompanyId;

                var assets = VivosEntities.Safes.Where(x => x.CompanyId == userCompanyId).Select(x => x.Asset);
                var assetsNotOwned = VivosEntities.Assets.Except(assets);

                foreach (var asset in assetsNotOwned)
                {
                    ret.Assets.Add(new AssetWithImage()
                    {
                        Id = asset.Id,
                        Description = asset.Description,
                        Image = Convert.ToBase64String(asset.AssetThumbnail.Thumbnail),
                        Name = asset.Name
                    });
                }

                ret.Message = "Operation succeed.";
                ret.HasError = false;
            }
            catch (Exception exc)
            {
                exc.LogException();
                throw;
            }

            return ret;
        }

    }
}