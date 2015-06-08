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
    public class PoiRepository : GenericRepository<Poi>, BusinessLogic.Repositories.IPoiRepository
    {
        public PoiRepository(ApplicationDbContext context)
            : base(context)
        {

        }
        public PoiRepository()
            : base(new ApplicationDbContext())
        {

        }
        

        public override IEnumerable<Poi> All()
        {
            context.Configuration.LazyLoadingEnabled = false;
            return context.Poi.Include(p => p.Tags);
        }
        public override Poi GetByID(object id)
        {
            return this.context.Poi.Select(p => p).Where(p => !p.IsDeleted).Where(p => p.ID == (int)id).SingleOrDefault();
        }
        public override Poi Insert(Poi entity)
        {
            Poi poi = base.Insert(entity);
            context.SaveChanges();
            return poi;
        }
        public List<Poi> get50FromSortNameAZ(int from)
        {
            return this.context.Poi.Where(i => !i.IsDeleted).OrderBy(i => i.Naam).Skip(from).Take(30).ToList();
        }
        public List<Poi> get50FromSortNameZA(int from)
        {
            return this.context.Poi.Where(i => !i.IsDeleted).OrderByDescending(i => i.Naam).Skip(from).Take(30).ToList();
        }
        public void Delete(Poi EntityToDelete)
        {
            base.Delete(EntityToDelete);
            context.SaveChanges();
        }
        public override void Update(Poi entityToUpdate)
        {
            base.Update(entityToUpdate);
            context.SaveChanges();
        }
        public void DeleteSoft(Poi poi)
        {
            poi.IsDeleted = true;
            Update(poi);
            
        }

    }
}
