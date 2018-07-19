using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using VivoosVR.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Core.Server.DataModel;

namespace VivoosVR.Web
{
    public class CoreApplicationUserManager : UserManager<CoreApplicationUser>
    {
        public CoreApplicationUserManager(IUserStore<CoreApplicationUser> store)
            : base(store)
        {
            
        }

        public override async Task<ClaimsIdentity> CreateIdentityAsync(CoreApplicationUser user, string authenticationType)
        {
            return await base.CreateIdentityAsync(user, authenticationType);
        }

        public static CoreApplicationUserManager Create(IdentityFactoryOptions<CoreApplicationUserManager> options, IOwinContext context)
        {
            var manager = new CoreApplicationUserManager(new CoreUserStore(context.Get<CoreEntities>())); // context.Get<ApplicationDbContext>()
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<CoreApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            //manager.PasswordValidator = new PasswordValidator
            //{
            //    RequiredLength = 6,
            //    RequireNonLetterOrDigit = true,
            //    RequireDigit = true,
            //    RequireLowercase = true,
            //    RequireUppercase = true,
            //};

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = false;

            //manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            //manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            //manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser>
            //{
            //    MessageFormat = "Your security code is {0}"
            //});
            //manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser>
            //{
            //    Subject = "Security Code",
            //    BodyFormat = "Your security code is {0}"
            //});

            //manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<CoreApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            manager.PasswordHasher = new CorePasswordHasher();

            return manager;
        }

    }
}