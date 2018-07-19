using Core.Server.Brokers;
using System.IdentityModel.Selectors;
using System.ServiceModel;

namespace Core.Server.Tools
{
    public class UserNameValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            switch (SessionBroker.Init().CheckUsernameAndPassword(userName, password))
            {
                case Constants.AuthenticationStatus.Succeed:
                case Constants.AuthenticationStatus.DefaultUser: // Default User is allowed for public contents
                    return;
                case Constants.AuthenticationStatus.UnknownUser:
                case Constants.AuthenticationStatus.Expired:
                default:
                    throw new FaultException("Unknown Username or Incorrect Password");
            }
        }
    }
}
