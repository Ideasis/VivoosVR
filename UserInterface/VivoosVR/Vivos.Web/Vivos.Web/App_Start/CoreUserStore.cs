using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Entity;
using VivoosVR.Web.App_Start;
using Microsoft.AspNet.Identity.EntityFramework;
using Core.Server.Brokers;
using Core.Common.DataModel.Core;
using Core.Server.Brokers.Organization;
using Core.Server.DataModel;

namespace VivoosVR.Web
{
    public class CoreUserStore : IUserStore<CoreApplicationUser>, IUserPasswordStore<CoreApplicationUser>, IUserLockoutStore<CoreApplicationUser, string>, IUserEmailStore<CoreApplicationUser>, IUserTwoFactorStore<CoreApplicationUser, string>
    {
        public CoreEntities CoreEntities { get; set; }
        public SessionBroker SessionBroker { get; set; }

        public CoreUserStore(CoreEntities dataModel)
        {
            CoreEntities = dataModel;
        }

        public CoreUserStore()
        {
        }

        public Task CreateAsync(CoreApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(CoreApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public async Task<CoreApplicationUser> GetUser(string id)
        {
            Task<CoreApplicationUser> t = new Task<CoreApplicationUser>(() =>
            {
                UserBroker userBroker = UserBroker
                    .Init()
                    .Load(id);

                CoreApplicationUser ret = new CoreApplicationUser()
                {
                    User = userBroker.User,
                    Email = userBroker.User.Consumer.Email,
                    Id = userBroker.User.Consumer.Code,
                    UserName = userBroker.User.Consumer.Code,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    PasswordHash = userBroker.User.Password,
                    TwoFactorEnabled = false,
                };

                var roles = userBroker.GetRoles();
                foreach (var role in roles)
                {
                    ret.Roles.Add(new IdentityUserRole()
                    {
                        RoleId = role.Code,
                        UserId = userBroker.User.Id.ToString()
                    });
                }

                return ret;
            });

            t.Start();

            return await t;
        }

        public async Task<CoreApplicationUser> FindByIdAsync(string userId)
        {
            return await GetUser(userId);
        }

        public async Task<CoreApplicationUser> FindByNameAsync(string userName)
        {
            return await GetUser(userName);
        }

        public async Task<string> GetPasswordHashAsync(CoreApplicationUser user)
        {
            Task<string> t = new Task<string>(() =>
            {
                return user.PasswordHash;
            });

            t.Start();

            return await t;
        }

        public Task<bool> HasPasswordAsync(CoreApplicationUser user)
        {
            return Task<bool>.Run(() => { return true; });
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task SetPasswordHashAsync(CoreApplicationUser user, string passwordHash)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            Task t = new Task(() =>
            {
                user.PasswordHash = passwordHash;
            });

            t.Start();

            return;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task UpdateAsync(CoreApplicationUser user)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<DateTimeOffset> GetLockoutEndDateAsync(CoreApplicationUser user)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            throw new NotImplementedException();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task SetLockoutEndDateAsync(CoreApplicationUser user, DateTimeOffset lockoutEnd)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<int> IncrementAccessFailedCountAsync(CoreApplicationUser user)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            throw new NotImplementedException();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task ResetAccessFailedCountAsync(CoreApplicationUser user)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return;
        }

        public async Task<int> GetAccessFailedCountAsync(CoreApplicationUser user)
        {
            Task<int> t = new Task<int>(() =>
            {
                return 10;
            });

            t.Start();

            return await t;
        }

        public async Task<bool> GetLockoutEnabledAsync(CoreApplicationUser user)
        {
            Task<bool> t = new Task<bool>(() =>
            {
                return false;
            });

            t.Start();

            return await t;
        }

        public Task SetLockoutEnabledAsync(CoreApplicationUser user, bool enabled)
        {
            throw new NotImplementedException();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task SetEmailAsync(CoreApplicationUser user, string email)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            Task t = new Task(() =>
            {
                user.Email = email;
            });

            t.Start();

            return;
        }

        public async Task<string> GetEmailAsync(CoreApplicationUser user)
        {
            Task<string> t = new Task<string>(() =>
            {
                return user.Email;
            });

            t.Start();

            return await t;
        }

        public async Task<bool> GetEmailConfirmedAsync(CoreApplicationUser user)
        {
            Task<bool> t = new Task<bool>(() =>
            {
                return true;
            });

            t.Start();

            return await t;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task SetEmailConfirmedAsync(CoreApplicationUser user, bool confirmed)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return;
        }

        public async Task<CoreApplicationUser> FindByEmailAsync(string email)
        {
            Task<CoreApplicationUser> t = new Task<CoreApplicationUser>(() =>
            {
                UserBroker userBroker = UserBroker.Init().Load(email);

                return new CoreApplicationUser()
                {
                    Email = userBroker.User.Consumer.Email,
                    PasswordHash = userBroker.User.Password,
                    UserName = userBroker.User.Consumer.Code,
                    Id = userBroker.User.Consumer.Code
                };
            });

            t.Start();

            return await t;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task SetTwoFactorEnabledAsync(CoreApplicationUser user, bool enabled)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<bool> GetTwoFactorEnabledAsync(CoreApplicationUser user)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return false;
        }
    }
}