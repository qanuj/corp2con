using System.Data.Entity;
using Autofac;
using e10.Shared;
using e10.Shared.Data;
using Talent21.Data.Repository;

namespace Talent21.Data
{
    public class DataAccessLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<SharedLayerModule>();
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();

            //TODO add all repositories.
            builder.RegisterType<SkillRepository>().As<ISkillRepository>();
            builder.RegisterType<TransactionRepository>().As<ITransactionRepository>();

            builder.RegisterType<ApplicationDbContext>().As<DbContext>().SingleInstance();
        }
    }
}
