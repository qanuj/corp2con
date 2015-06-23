using Autofac;
using e10.Shared.Data;
using e10.Shared.Security;

namespace e10.Shared
{
    public class SharedLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationUserStore>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationRoleStore>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<EmailService>().As<IIdentityEmailMessageService>().InstancePerRequest();
            builder.RegisterType<SmsService>().As<IIdentitySmsMessageService>().InstancePerRequest();
        }
    }
}
