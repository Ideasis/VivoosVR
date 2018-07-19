using Microsoft.AspNet.Identity.Owin;
using VivoosVR.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VivoosVR.Web.Controllers
{
    public class BaseController : Controller
    {
        public CoreApplicationUser GetCurrentUser(HttpContextBase context)
        {
            var ret = UserHelper.GetCurrentUser();

            return ret;
        }

        public CoreApplicationUser GetCurrentUser()
        {
            if (HttpContext == null) return null;

            return GetCurrentUser(HttpContext);
        }

        protected override void Initialize(RequestContext requestContext)
        {
            var r = GetCurrentUser(requestContext.HttpContext);

            if (r!=null)
            {
                ViewBag.User = r.User;
            }

            base.Initialize(requestContext);
        }
    }
}