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
    public class PoiRepository : GenericRepository<Poi>
    {
        public PoiRepository(ApplicationDbContext context)
            : base(context)
        {

        }
        public PoiRepository()
            : base(new ApplicationDbContext())
        {

        }
        

    }
}
