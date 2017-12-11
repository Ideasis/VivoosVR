using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace VivoosVR.Web
{
    public class UserHelper
    {
        public static CoreApplicationUser GetCurrentUser()
        {
            CoreApplicationUser t = null;

            var identity = HttpContext.Current.GetOwinContext().Authentication.User.Identity;
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<CoreApplicationUserManager>();

            Task<CoreApplicationUser> task = Task<CoreApplicationUser>.Run(async () =>
            {
                if (!string.IsNullOrWhiteSpace(identity.Name))
                {
                    var ret = await userManager.FindByIdAsync(identity.Name);
                    return ret;
                }

                return null;
            });

            task.Wait();
            t = task.Result;

            return t;
        }
    }
}