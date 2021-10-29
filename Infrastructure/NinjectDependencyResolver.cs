using EssentialToolsMVC.Models;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EssentialToolsMVC.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IValueCalculator>().To<LinqValueCalculator>().InRequestScope();
            kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>()
                // example .WithPropertyValue("DiscountSize", 50m);
                .WithConstructorArgument("discountParam", 50m); //example of passing constructor params from DI resolver

            // conditional binding
            kernel.Bind<IDiscountHelper>().To<FlexibleDiscountHelper>().WhenInjectedInto<LinqValueCalculator>();
        }
    }
}

// object scopes
/*
 InTransientScope() T his is the same as not specifying a scope and creates a new object for each dependency that is resolved.

InSingletonScope() Creates a single instance which is shared throughout the application. Ninject will create the instance if you use
ToConstant(object) InSingletonScope or you can provide it with the ToConstant method.


InThreadScope() Creates a single instance which is used to resolve dependencies for objects requested by a single thread.
InRequestScope() Creates a single instance which is used to resolve dependencies for objects requested by a single HT T P request.
 
 
 */