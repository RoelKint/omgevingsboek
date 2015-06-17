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
            
            return (from r in context.Routes.Include(r => r.RouteLijst).Include(r => r.Boeken) where r.Boeken.Any(x => x.Id == boekId) where r.IsDeleted==false where r.IsDeleted == false select r).ToList();
        }
        public override Route Insert(Route entity)
        {
            Route res = new Route();
            res.Boeken = new List<Boek>();
            res.Naam = entity.Naam;
            res.EigenaarID = entity.EigenaarID;
            res.DeelLijst = new List<ApplicationUser>();
            res.DeelLijst.Add(context.Users.Find(entity.Eigenaar));
            res.RouteLijst = new List<RouteListItem>();

            foreach (Boek b in entity.Boeken)
            {
                res.Boeken.Add(context.Boeken.Where(x => x.Id == b.Id).FirstOrDefault());
            }

            int count = 0;
            foreach (RouteListItem rli in entity.RouteLijst)
            {
                res.RouteLijst.Add(context.RouteListItem.Add(new RouteListItem()
                {
                    Activiteit = context.Activiteiten.Where(x => x.Id == rli.Activiteit.Id).FirstOrDefault(),
                    OrderIndex = count
                }));
                count++;
            }


            Route resroute = base.Insert(res);
            context.SaveChanges();
            
            return resroute;
        }

        public override Route GetByID(object id)
        {
            
            return context.Routes.Include(r => r.RouteLijst).Include(r => r.RouteLijst.Select(x => x.Activiteit)).Include(r => r.DeelLijst).Where(r => r.IsDeleted == false).Where(r => r.Id == (int)id).SingleOrDefault();
                
        }
        public void SoftDelete(int id)
        {
            Route r = GetByID(id);
            r.IsDeleted = true;
            Update(r);
            context.SaveChanges();
        }


    }
}
