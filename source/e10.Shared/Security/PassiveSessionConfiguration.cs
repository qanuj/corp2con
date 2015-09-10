using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.IdentityModel.Services.Tokens;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e10.Shared.Security
{
    public class PassiveSessionConfiguration
    {
        public static void ConfigureMackineKeyProtectionForSessionTokens()
        {
            var handler = (SessionSecurityTokenHandler)FederatedAuthentication.FederationConfiguration.IdentityConfiguration.SecurityTokenHandlers[typeof(SessionSecurityToken)];
            if (!(handler is MachineKeySessionSecurityTokenHandler))
            {
                var mkssth = new MachineKeySessionSecurityTokenHandler();
                if (handler != null) mkssth.TokenLifetime = handler.TokenLifetime;
                FederatedAuthentication.FederationConfiguration.IdentityConfiguration.SecurityTokenHandlers.AddOrReplace(mkssth);
            }
        }
    }
}
