using System.Data.Entity;
using Autofac;
using e10.Shared;
using e10.Shared.Data;
using e10.Shared.Data.Abstraction;
using Talent21.Data.Repository;

namespace Talent21.Data
{
    public class DataAccessLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<SharedLayerModule>();

            //TODO add all repositories.
            builder.RegisterType<DefaultEventManager>().As<IEventManager>();

            builder.RegisterType<BlockRepository>().As<IBlockRepository>();
            builder.RegisterType<ContractorRepository>().As<IContractorRepository>();
            builder.RegisterType<ContractorSkillRepository>().As<IContractorSkillRepository>();
            builder.RegisterType<JobSkillRepository>().As<IJobSkillRepository>();
            builder.RegisterType<ContractorVisitRepository>().As<IContractorVisitRepository>();
            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>();
            builder.RegisterType<CompanyVisitRepository>().As<ICompanyVisitRepository>();
            builder.RegisterType<ContractRepository>().As<IContractRepository>();
            builder.RegisterType<ContractScheduleRepository>().As<IContractScheduleRepository>();
            builder.RegisterType<IndustryRepository>().As<IIndustryRepository>();
            builder.RegisterType<JobApplicationRepository>().As<IJobApplicationRepository>();
            builder.RegisterType<JobRepository>().As<IJobRepository>();
            builder.RegisterType<JobVisitRepository>().As<IJobVisitRepository>();
            builder.RegisterType<FunctionalAreaRepository>().As<IFunctionalAreaRepository>();
            builder.RegisterType<LocationRepository>().As<ILocationRepository>();
            builder.RegisterType<PaymentRepository>().As<IPaymentRepository>();
            builder.RegisterType<PlanRepository>().As<IPlanRepository>();
            builder.RegisterType<ScheduleRepository>().As<IScheduleRepository>();
            builder.RegisterType<SkillRepository>().As<ISkillRepository>();
            builder.RegisterType<SubscriptionRepository>().As<ISubscriptionRepository>();
            builder.RegisterType<TransactionRepository>().As<ITransactionRepository>();

            builder.RegisterType<ApplicationDataContext>().As<ApplicationDbContext>().As<DbContext>().InstancePerRequest();
        }
    }
}
