using Autofac;
using e10.Shared.Providers;
using e10.Shared.Respository;
using e10.Shared.Security;
using Microsoft.Owin.Logging;

namespace e10.Shared
{
    public class SharedLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationUserStore>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationRoleStore>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().As<IUserService>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<SendGridEmailService>().As<IIdentityEmailMessageService>().InstancePerRequest();
            builder.RegisterType<SmsService>().As<IIdentitySmsMessageService>().InstancePerRequest();
            builder.RegisterType<InviteRepository>().As<IInviteRepository>().InstancePerRequest();
            builder.RegisterType<DoNotReplyAte10EmailConfigProvider>().As<IEmailConfigProvider>().InstancePerRequest();
            builder.RegisterType<ElmahLogger>().As<ILogger>().InstancePerRequest();
        }
    }
}
