using Autofac;
using VideoConferencingDemo.Web.Models;

namespace VideoConferencingDemo.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SignUpModel>().AsSelf();
            builder.RegisterType<SignInModel>().AsSelf();

            base.Load(builder);
        }
    }
}
