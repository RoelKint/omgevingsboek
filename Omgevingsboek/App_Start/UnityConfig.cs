using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using BusinessLogic.Repositories;
using BusinessLogic.Services;
using Omgevingsboek.Controllers;

namespace Omgevingsboek
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IActiviteitRepository, ActiviteitRepository>();
            container.RegisterType<IBoekRepository, BoekRepository>();
            container.RegisterType<ITagRepository, TagRepository>();
            container.RegisterType<IBoekService, BoekService>();
            container.RegisterType<AccountController>(new InjectionConstructor());

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}