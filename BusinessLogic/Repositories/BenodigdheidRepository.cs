using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class BenodigdheidRepository : GenericRepository<Benodigdheid>, BusinessLogic.Repositories.IBenodigdheidRepository
    {
         public BenodigdheidRepository(ApplicationDbContext context)
            : base(context)
        {

        }
         public BenodigdheidRepository()
            : base(new ApplicationDbContext())
        {

        }

        public Benodigdheid Insert(string entity)
        {
            Benodigdheid benodigdheid = context.Benodigdheden.Select(t => t).Where(t => t.Naam.Equals(entity)).SingleOrDefault();
            if (benodigdheid != null) return benodigdheid;
            Benodigdheid newbenodigdheid = context.Benodigdheden.Add(new Benodigdheid() { Naam = entity });
            context.SaveChanges();
            return newbenodigdheid;
        }
        
        public override IEnumerable<Benodigdheid> All()
        {
            return base.All();
        }

    }
}
