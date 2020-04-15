using LogSystem.BLL.Interfaces;
using LogSystem.BLL.Services;
using Ninject;
using Ninject.Extensions.ChildKernel;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace LogSystem.PL.Utils
{
    public class NinjectDependencyResolver : IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver(): this(new StandardKernel())
        { }

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }


        private void AddBindings()
        {
            _kernel.Bind<IUserService>().To<UserService>();
            _kernel.Bind<IValidationService>().To<ValidationService>();
            _kernel.Bind<IUserActionService>().To<UserActionService>();
        }

        private IKernel AddRequestBindings(IKernel kernel)
        {
            _kernel.Bind<IUserService>().To<UserService>().InSingletonScope();
            _kernel.Bind<IValidationService>().To<ValidationService>().InSingletonScope();
            _kernel.Bind<IUserActionService>().To<UserActionService>().InSingletonScope();
      
            return kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyResolver(AddRequestBindings(new ChildKernel(_kernel)));
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }


        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public void Dispose()
        {
        }
    }
}