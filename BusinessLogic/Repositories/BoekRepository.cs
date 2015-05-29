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
    class BoekRepository : GenericRepository<Boek>
    {
        public BoekRepository(ApplicationDbContext context)
            : base(context)
        {

        }
        public BoekRepository()
            : base(new ApplicationDbContext())
        {

        }

        public override Boek GetByID(object id)
        {
            return this.context.Boeken.Include(b => b.DeelLijst).Include(b => b.Activiteiten).Single();
        }
        public List<Boek> getBoekenByUser(string username)
        {
            return (from b in context.Boeken.Include(b => b.DeelLijst).Include(b => b.Activiteiten) where b.Eigenaar.UserName == username select b).ToList();
        }
        //sharedboeken omvatten niet de eigen boeken.
        public List<Boek> getSharedBoeken(string username)
        {
            return (from b in context.Boeken.Include(b => b.DeelLijst).Include(b => b.Activiteiten) where b.DeelLijst.Contains(context.Users.Select(i=>i).Where(i => i.UserName == username).FirstOrDefault()) where b.Eigenaar.UserName != username select b).ToList();
        }



    }
}
