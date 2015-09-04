using System;
using e10.Shared.Data;
using e10.Shared.Data.Abstraction;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Owin.Security.Providers.GitHub;
using Owin.Security.Providers.GooglePlus;
using Owin.Security.Providers.LinkedIn;

namespace e10.Shared.Security
{
    public class SecurityManager
    {
        public static string God = "Admin";

        public static OAuthAuthorizationServerOptions Setup(IAppBuilder app,string publicClientId,Func<ApplicationDbContext> create)
        {

            app.CreatePerOwinContext<ApplicationDbContext>(create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, User>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            
            var OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(publicClientId),
                AuthorizeEndpointPath = new PathString("/Account/Authorize"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
            app.UseOAuthBearerTokens(OAuthOptions);

            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            return OAuthOptions;
        }

        public static OAuthWebConfigProvider SetupSocial(IAppBuilder app, OAuthWebConfigProvider SocialProvider)
        {
            if (!string.IsNullOrWhiteSpace(SocialProvider.GooglePlus.Key))
                app.UseGooglePlusAuthentication(SocialProvider.GooglePlus.Key, SocialProvider.GooglePlus.Secret);
            if (!string.IsNullOrWhiteSpace(SocialProvider.LinkedIn.Key))
                app.UseLinkedInAuthentication(SocialProvider.LinkedIn.Key, SocialProvider.LinkedIn.Secret);
            if (!string.IsNullOrWhiteSpace(SocialProvider.GitHub.Key))
                app.UseGitHubAuthentication(SocialProvider.GitHub.Key, SocialProvider.GitHub.Secret);
            if (!string.IsNullOrWhiteSpace(SocialProvider.Facebook.Key))
                app.UseFacebookAuthentication(SocialProvider.Facebook.Key, SocialProvider.Facebook.Secret);
            if (!string.IsNullOrWhiteSpace(SocialProvider.Microsoft.Key))
                app.UseMicrosoftAccountAuthentication(SocialProvider.Microsoft.Key, SocialProvider.Microsoft.Secret);
            if (!string.IsNullOrWhiteSpace(SocialProvider.Twitter.Key))
                app.UseTwitterAuthentication(SocialProvider.Twitter.Key, SocialProvider.Twitter.Secret);
            return SocialProvider;
        }
    }
}