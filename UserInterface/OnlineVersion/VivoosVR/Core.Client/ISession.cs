using Core.Common.DataModel.Core;
using System;

namespace Core.Client
{
    public interface ISession
    {
        User CurrentUser { get; }

        bool AmIAllowed(string resource);

        bool DoIHaveRole(string role);

        T GetProxy<T>(string configurationName = "*");

        Session SignIn(string username, string password);

        void SignOut();
    }
}
