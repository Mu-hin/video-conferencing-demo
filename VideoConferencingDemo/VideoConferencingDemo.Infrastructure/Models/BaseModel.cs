using Autofac;
using AutoMapper;

namespace VideoConferencingDemo.Infrastructure.Models
{
    public class BaseModel
    {
        protected ILifetimeScope _scope;
        protected IMapper _mapper;

        public virtual void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
        }
    }
}
