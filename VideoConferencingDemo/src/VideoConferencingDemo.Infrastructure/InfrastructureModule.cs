using Autofac;
using VideoConferencingDemo.Infrastructure.Adapter;
using VideoConferencingDemo.Infrastructure.DbContexts;
using VideoConferencingDemo.Infrastructure.Repositories;
using VideoConferencingDemo.Infrastructure.Services;
using VideoConferencingDemo.Infrastructure.UnitOfWorks;

namespace VideoConferencingDemo.Infrastructure
{
    public class InfrastructureModule : Module
    {
        
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public InfrastructureModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<AdapterUserManager>().As<IUserManager>().InstancePerLifetimeScope();
            builder.RegisterType<AdapterSignInManager>().As<ISignInManager>().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<MeetingLinkRepository>().As<IMeetingLinkRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MeetingLinksService>().As<IMeetingLinksService>().InstancePerLifetimeScope();
            builder.RegisterType<TimeService>().As<ITimeService>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
