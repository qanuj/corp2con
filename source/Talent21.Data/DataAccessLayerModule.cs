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
            
            builder.RegisterType<DefaultEventManager>().As<IEventManager>();

            builder.RegisterType<ContractorFolderRepository>().As<IContractorFolderRepository>();
            builder.RegisterType<ConversationRepository>().As<IConversationRepository>();
            builder.RegisterType<FeedbackRepository>().As<IFeedbackRepository>();
            builder.RegisterType<BlockRepository>().As<IBlockRepository>();
            builder.RegisterType<AdvertisementRepository>().As<IAdvertisementRepository>();
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
            builder.RegisterType<JobApplicationHistoryRespository>().As<IJobApplicationHistoryRespository>();
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
            builder.RegisterType<CountryRepository>().As<ICountryRepository>();
            builder.RegisterType<MemberRepository>().As<IMemberRepository>();
            builder.RegisterType<AppSiteConfigRepository>().As<IAppSiteConfigRepository>();

            builder.RegisterType<ApplicationDataContext>().As<ApplicationDbContext>().As<DbContext>().InstancePerRequest();
        }
    }
}
