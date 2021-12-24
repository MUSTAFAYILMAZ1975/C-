using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nortwind.Business.DependencyResolvers.Ninject
{
   public class InstanceFactory
    {
        public static T GetInstance<T> ()                                           //(NinjectModule module)
        {
            var kernel = new StandardKernel(new BusinessModule());                    //(module);
            return kernel.Get<T>();
         }

       
    }
}
