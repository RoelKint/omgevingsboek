using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using BusinessLogic.Repositories;
using BusinessLogic.Services;
using Omgevingsboek.Controllers;
using Models.OmgevingsBoek_Models;

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
            container.RegisterType<IPoiRepository, PoiRepository>();
            container.RegisterType<IUitnodigingRepository, UitnodigingRepository>();
            container.RegisterType<IBenodigdheidRepository, BenodigdheidRepository>();
            container.RegisterType<IGenericRepository<PoiTags>, GenericRepository<PoiTags>>();
            container.RegisterType<IBoekOrderRepository, BoekOrderRepository>();
            container.RegisterType<IVraagRepository, VraagRepository>();

            //toegevoegd door roel !!!!!! VERANDERING
            container.RegisterType<IRouteRepository, RouteRepository>();

            

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}