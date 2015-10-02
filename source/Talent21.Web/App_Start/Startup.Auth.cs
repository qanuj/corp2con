using e10.Shared;
using e10.Shared.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Talent21.Data;

namespace Talent21.Web
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static string PublicClientId { get; private set; }
        public static OAuthWebConfigProvider SocialProvider { get; private set; }
        public static string FacebookAppId { get; set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            PassiveSessionConfiguration.ConfigureMackineKeyProtectionForSessionTokens();
            PublicClientId = "corp2con";
            OAuthOptions = SecurityManager.Setup(app, PublicClientId, ApplicationDataContext.Create);
            SocialProvider = SecurityManager.SetupSocial(app, new OAuthWebConfigProvider());
        }
    }
}