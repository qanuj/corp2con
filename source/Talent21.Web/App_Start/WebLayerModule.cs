using Autofac;
using Talent21.Service.Abstraction;
using Talent21.Web.Mailers;

namespace Talent21.Web
{
    public class WebLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Notifications>().As<INotificationService>();
        }
    }
}