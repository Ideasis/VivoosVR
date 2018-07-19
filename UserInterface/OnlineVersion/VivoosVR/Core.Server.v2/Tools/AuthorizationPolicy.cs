using Core.Log;
using Core.Server.Brokers.Organization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;

namespace Core.Server.Tools
{
    public class AuthorizationPolicy : IAuthorizationPolicy
    {
        class AuthenticationState
        {
            bool _ClaimsAdded;

            public bool ClaimsAdded
            {
                get { return _ClaimsAdded; }
                set { _ClaimsAdded = value; }
            }
        }

        string _Id;

        public string Id
        {
            get { return _Id; }
        }
        public ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }

        public AuthorizationPolicy()
        {
            _Id = Guid.NewGuid().ToString();
        }

        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            bool ret = false;
            AuthenticationState customstate = null;

            if (state == null)
            {
                customstate = new AuthenticationState();
                state = customstate;
            }
            else
            {
                customstate = (AuthenticationState)state;
            }

            // If we've not added claims yet...
            if (!customstate.ClaimsAdded)
            {
                IList<Claim> claims = new List<Claim>();

                foreach (ClaimSet cs in evaluationContext.ClaimSets)
                {
                    foreach (Claim c in cs.FindClaims(ClaimTypes.Name, Rights.PossessProperty))
                    {
                        "** User: {0}".LogInformation(c.Resource.ToString());

                        foreach (string s in GetAllowedOperations(c.Resource.ToString()))
                        {
                            claims.Add(new Claim("Claims", s, Rights.PossessProperty));
                        }
                    }
                }

                evaluationContext.AddClaimSet(this, new DefaultClaimSet(this.Issuer, claims));
                customstate.ClaimsAdded = true;

                ret = true;
            }
            else
            {
                // Should never get here, but just in case...
                ret = true;
            }


            return ret;
        }

        private static IEnumerable<string> GetAllowedOperations(string username)
        {
            IEnumerable<string> ret = UserBroker
                .Init()
                .Load(username)
                .GetClaims()
                .Select(x => x.Resource);

            return ret;
        }
    }
}
