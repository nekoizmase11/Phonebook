using Phonebook.Bussines.Contracts.DbContextInterface;
using Phonebook.Data.SQLServerDbContext;
using System.Configuration;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace Phonebook.Presentation.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IContext, SQLServerContext>(new InjectionConstructor(ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString));


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}