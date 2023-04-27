using Autofac;

namespace VideoConferencingDemo.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<TestClass>().As<ITestClass>().InstancePerLifetimeScope();

            //builder.RegisterType<IndexModel>().AsSelf();
            base.Load(builder);
        }
    }
}
