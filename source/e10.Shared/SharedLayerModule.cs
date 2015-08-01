using System;
using Autofac;
using e10.Shared.Data;
using e10.Shared.Data.Abstraction;
using e10.Shared.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

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
            builder.RegisterType<EmailService>().As<IIdentityEmailMessageService>().InstancePerRequest();
            builder.RegisterType<SmsService>().As<IIdentitySmsMessageService>().InstancePerRequest();
        }
    }
}
