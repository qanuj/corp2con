using System;
using e10.Shared.Data.Abstraction;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;

namespace e10.Shared.Security
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(ApplicationUserStore store, IIdentitySmsMessageService smsService, IIdentityEmailMessageService emailService)
            : base(store)
        {
            // Configure validation logic for usernames
            this.UserValidator = new UserValidator<User>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            this.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            this.UserLockoutEnabledByDefault = true;
            this.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            this.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            this.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<User>
            {
                MessageFormat = "Your security code is {0}"
            });
            this.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            this.EmailService = emailService;
            this.SmsService = smsService;
            this.UserTokenProvider = DefaultTokenProvider();
        }

        public static IUserTokenProvider<User, string> DefaultTokenProvider()
        {
            return new DataProtectorTokenProvider<User>(new DpapiDataProtectionProvider("FbHireSuperLockerd").Create("EmailConfirmation")); ;
        }
    }
}