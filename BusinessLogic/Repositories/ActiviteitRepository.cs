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
    class ActiviteitRepository : GenericRepository<Activiteit>
    {
        public ActiviteitRepository(ApplicationDbContext context)
            : base(context)
        {

        }

        public override IEnumerable<Activiteit> All()
        {
            return this.context.Activiteiten.Include(i => i.Boeken);
        }
    }
}
