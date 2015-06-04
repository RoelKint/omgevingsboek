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
        public override Poi Insert(Poi entity)
        {
            Poi poi = base.Insert(entity);
            context.SaveChanges();
            return poi;
        }
        public List<Poi> get50()
        {
            return this.context.Poi.OrderBy(i => i.Naam).Take(50).ToList();
        }
        public List<Poi> get50From(int from)
        {
            return this.context.Poi.OrderBy(i => i.Naam).Skip(from).Take(50).ToList();
        }

    }
}
