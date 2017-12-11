using Core.Log.Configuration;
using Core.Log.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.Linq;
using System.ServiceModel;

namespace Core.Server.Tools
{
    public class AuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            string action = operationContext.RequestContext.RequestMessage.Headers.Action;

#if DEBUG
            // Looking for metadata
            if (action == "http://schemas.xmlsoap.org/ws/2004/09/transfer/Get")
            {
                return true;
            }
#endif

            IEnumerable<ClaimSet> systemClaimSets = operationContext.ServiceSecurityContext.AuthorizationContext.ClaimSets.Where(x => x.Issuer == ClaimSet.System);

            foreach (ClaimSet cs in systemClaimSets)
            {
                if (cs.Issuer == ClaimSet.System)
                {
                    foreach (Claim c in cs.FindClaims("Claims", Rights.PossessProperty))
                    {
                        Trace.WriteLine(string.Format("** Checking claim : {0}", c.Resource));

                        if (action == c.Resource.ToString())
                        {
                            Trace.WriteLine(string.Format("** Authorization succeed for resource : {0}", action));

                            return true;
                        }
                    }
                }
            }

#if IGNORE_AUTHORIZATION
            Trace.WriteLine(string.Format("** Authorization omitted for resource : {0}", action));
            return true;
#else
            Trace.WriteLine(string.Format("** Authorization failed for resource : {0}", action));
            throw new ApplicationException(LogConfiguration.Configuration.SecurityExceptionMessage);
            //return false;
#endif
        }
    }
}
