using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Practices.Unity;
using WebApi.Abstract;
using WebApi.Implementation;
using WebApi.IoC;


namespace WebApi.App_Start
{
    public class DependecyContainerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IDataManager, PokemonDataManager>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            //var dataManager = container.Resolve<IDataManager>();

            // Other Web API configuration not shown.
        }
    }
}