using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    class RouteRepository : GenericRepository<Route>
    {
        public RouteRepository(ApplicationDbContext context)
            : base(context)
        {

        }
        public RouteRepository()
            : base(new ApplicationDbContext())
        {

        }

        


    }
}
