using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Core.Server.DataModel;
using Market.Server;

namespace VivoosVR.Web.APIControllers
{
    public class BaseApiController : ApiController
    {
        public CoreEntities CoreEntities { get; set; }
        public VivosEntities VivosEntities { get; set; }

        public BaseApiController()
        {
            CoreEntities = new CoreEntities();
            VivosEntities = new VivosEntities();
        }

        public CoreApplicationUser GetCurrentUser()
        {
            return UserHelper.GetCurrentUser();
        }
    }
}