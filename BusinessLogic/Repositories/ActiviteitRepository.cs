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
            context.Configuration.LazyLoadingEnabled = false;
        }
        public ActiviteitRepository()
            : base(new ApplicationDbContext())
        {

        }

        public override IEnumerable<Activiteit> All()
        {
            return this.context.Activiteiten.Include(i => i.Boeken).Include(i => i.Benodigdheden).Include(i => i.DeelLijst).Include(i => i.Eigenaar).Include(i => i.Fotoboeken).Include(i => i.Poi).Include(i => i.Routes).Include(i => i.Tags).Include(i => i.Videos);
        }

        public List<Activiteit> getActiviteitenPerPoi(int id)

        {
            context.Configuration.LazyLoadingEnabled = false;
            return (from a in context.Activiteiten where !a.IsDeleted where a.PoiId == id select a).ToList();
        }

        public override Activiteit GetByID(object id)
        {
            return this.context.Activiteiten.Where(i => i.Id == (int)id).Where(i => !i.IsDeleted).Single();
        }
        public List<Activiteit> getActivitiesByUsername(string Username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from a in context.Activiteiten where !a.IsDeleted where a.Eigenaar.UserName == Username select a).ToList();
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
                        where !a.IsDeleted
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
                        where !a.IsDeleted
                        select a).ToList();
            }
        }
        
        public List<Activiteit> get50FromSortNameAZ(int from)
        {
            return this.context.Activiteiten.Where(i => !i.IsDeleted).OrderBy(i => i.Naam).Skip(from).Take(50).ToList();
        }
        public List<Activiteit> get50FromSortNameZA(int from)
        {
            return this.context.Activiteiten.Where(i => !i.IsDeleted).OrderByDescending(i => i.Naam).Skip(from).Take(50).ToList();
        }
        public List<Activiteit> get50FromSortUserAZ(int from)
        {
            return this.context.Activiteiten.Where(i => !i.IsDeleted).OrderBy(i => i.Eigenaar.UserName).Skip(from).Take(50).ToList();
        }
        public List<Activiteit> get50FromSortUserZA(int from)
        {
            return this.context.Activiteiten.Where(i => !i.IsDeleted).OrderByDescending(i => i.Eigenaar.UserName).Skip(from).Take(50).ToList();
        }
        public List<Activiteit> get50FromSortPoiAZ(int from)
        {
            return this.context.Activiteiten.Where(i => !i.IsDeleted).OrderBy(i => i.Poi.Naam).Skip(from).Take(50).ToList();
        }
        public List<Activiteit> get50FromSortPoiZA(int from)
        {
            return this.context.Activiteiten.Where(i => !i.IsDeleted).OrderByDescending(i => i.Poi.Naam).Skip(from).Take(50).ToList();
        }
        public List<Activiteit> getUserActiviteitenByUser50from(int from, String Owner, String Visitor)
        {
            return this.context.Activiteiten.Where(i => !i.IsDeleted).Where(i => i.Eigenaar.UserName == Owner).Where(i=> i.DeelLijst.Contains(context.Users.Select(u => u).Where(u => u.UserName == Visitor).FirstOrDefault()) ).OrderBy(i => i.Naam).Skip(from).Take(50).ToList();
        }

        public void DeleteSoft(Activiteit entityToDelete)
        {
            entityToDelete.IsDeleted = true;
            Update(entityToDelete);
            context.SaveChanges();

        }

        public override void Delete(Activiteit id)
        {
            base.Delete(id);
            context.SaveChanges();

        }

        public List<Activiteit> getActiviteitenByPoiByUser50from(int from, String Owner, int PoiId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from a in context.Activiteiten where !a.IsDeleted where a.Eigenaar.UserName == Owner where a.Poi.ID == PoiId select a).ToList();
            }
        }

    }
}
