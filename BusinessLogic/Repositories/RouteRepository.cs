using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace BusinessLogic.Repositories
{
    public class RouteRepository : GenericRepository<Route>, BusinessLogic.Repositories.IRouteRepository
    {
        public RouteRepository(ApplicationDbContext context)
            : base(context)
        {

        }
        public RouteRepository()
            : base(new ApplicationDbContext())
        {

        }

        public List<Route> getRoutesByBoek(int boekId)
        {
            context.Configuration.LazyLoadingEnabled = false;
            
            return (from r in context.Routes.Include(r => r.RouteLijst).Include(r => r.Boek) where r.BoekId == boekId where r.IsDeleted == false select r).ToList();
        }
        public override Route Insert(Route entity)
        {
            Route res = new Route();
            res.BoekId = entity.BoekId;
            res.Naam = entity.Naam;
            res.EigenaarID = entity.Eigenaar.Id;
            res.DeelLijst = new List<ApplicationUser>();
            res.DeelLijst.Add(context.Users.Find(entity.Eigenaar));
            res.RouteLijst = new List<RouteListItem>();


            foreach (RouteListItem rli in entity.RouteLijst)
            {
                res.RouteLijst.Add(context.RouteListItem.Add(new RouteListItem()
                {
                    Activiteit = context.Activiteiten.Find(rli.Activiteit),
                    OrderIndex = rli.OrderIndex
                }));
            }


            return Insert(res);
        }
        


    }
}
