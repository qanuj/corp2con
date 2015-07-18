using Autofac;
using Talent21.Data;
using Talent21.Service.Abstraction;
using Talent21.Service.Core;

namespace Talent21.Service
{
    public class ServiceLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DataAccessLayerModule>();
            builder.RegisterType<ContractorService>().As<IContractorService>();
            builder.RegisterType<SystemService>().As<ISystemService>();
            builder.RegisterType<JobService>().As<IJobService>();
            builder.RegisterType<CompanyService>().As<ICompanyService>();
        }
    }
}
