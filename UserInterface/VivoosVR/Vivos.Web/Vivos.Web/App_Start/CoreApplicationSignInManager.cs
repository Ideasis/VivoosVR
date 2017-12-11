using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace VivoosVR.Web
{
    // Configure the application sign-in manager which is used in this application.
    public class CoreApplicationSignInManager : SignInManager<CoreApplicationUser, string>
    {
        public CoreApplicationSignInManager(CoreApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
            
        }

        public override async Task<ClaimsIdentity> CreateUserIdentityAsync(CoreApplicationUser user)
        {
            return await user.GenerateUserIdentityAsync((CoreApplicationUserManager)UserManager);
        }

        public static CoreApplicationSignInManager Create(IdentityFactoryOptions<CoreApplicationSignInManager> options, IOwinContext context)
        {
            return new CoreApplicationSignInManager(context.GetUserManager<CoreApplicationUserManager>(), context.Authentication);
        }

        public override async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            if (string.IsNullOrWhiteSpace( userName) || string.IsNullOrWhiteSpace(password))
            {
                return SignInStatus.Failure;
            }
            else
            {
                return await base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
            }
        }

    }
}