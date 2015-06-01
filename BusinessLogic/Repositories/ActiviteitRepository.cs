using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace BusinessLogic.Repositories
{
    public class ActiviteitRepository : GenericRepository<Activiteit>, BusinessLogic.Repositories.IActiviteitRepository
    {
        public ActiviteitRepository(ApplicationDbContext context)
            : base(context)
        {

        }
        public ActiviteitRepository()
            : base(new ApplicationDbContext())
        {

        }

        public override IEnumerable<Activiteit> All()
        {
            return this.context.Activiteiten.Include(i => i.Boeken).Include(i => i.Benodigdheden).Include(i => i.DeelLijst).Include(i => i.Eigenaar).Include(i => i.Fotoboeken).Include(i => i.Poi).Include(i => i.Routes).Include(i => i.Tags).Include(i => i.Videos);
        }
        public override Activiteit GetByID(object id)
        {
            return this.context.Activiteiten.Where(i => i.Id == (int)id).Include(i => i.Boeken).Include(i => i.Benodigdheden).Include(i => i.DeelLijst).Include(i => i.Eigenaar).Include(i => i.Fotoboeken).Include(i => i.Poi).Include(i => i.Routes).Include(i => i.Tags).Include(i => i.Videos).Single();
        }
        public List<Activiteit> getActivitiesByUsername(string Username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return (from a in context.Activiteiten.Include(i => i.Boeken).Include(i => i.Benodigdheden).Include(i => i.DeelLijst).Include(i => i.Eigenaar).Include(i => i.Fotoboeken).Include(i => i.Poi).Include(i => i.Routes).Include(i => i.Tags).Include(i => i.Videos) where a.Eigenaar.UserName == Username select a).ToList();
            }
        }
        public List<Activiteit> getSharedActivitiesByUsername(string Username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return (from a in context.Activiteiten.Include(i => i.Boeken)
                            .Include(i => i.Benodigdheden)
                            .Include(i => i.DeelLijst)
                            .Include(i => i.Eigenaar)
                            .Include(i => i.Fotoboeken)
                            .Include(i => i.Poi)
                            .Include(i => i.Routes)
                            .Include(i => i.Tags)
                            .Include(i => i.Videos) 
                        where a.DeelLijst.Contains(context.Users.Select(i=>i).Where(i=>i.UserName == Username).FirstOrDefault())
                        where a.Eigenaar != context.Users.Select(i => i).Where(i => i.UserName == Username).FirstOrDefault()
                        select a).ToList();
            }
        }
        public List<Activiteit> getSharedActivitiesByBookId(int BoekId,string Username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return (from a in context.Activiteiten.Include(i => i.Boeken)
                            .Include(i => i.Benodigdheden)
                            .Include(i => i.DeelLijst)
                            .Include(i => i.Eigenaar)
                            .Include(i => i.Fotoboeken)
                            .Include(i => i.Poi)
                            .Include(i => i.Routes)
                            .Include(i => i.Tags)
                            .Include(i => i.Videos)
                        where a.DeelLijst.Contains(context.Users.Select(i => i).Where(i => i.UserName == Username).FirstOrDefault())
                        where a.Boeken.Contains(context.Boeken.Select(i => i).Where(i => i.Id == BoekId).FirstOrDefault())
                        select a).ToList();
            }
        }
        
    }
}
