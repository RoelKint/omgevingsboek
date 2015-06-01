using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Validation;


namespace BusinessLogic.Repositories
{
    public class BoekRepository : GenericRepository<Boek>, IBoekRepository
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
        public override Boek Insert(Boek entity)
        {
            Boek res = base.Insert(entity);
            if(res.DeelLijst != null)
            foreach (var item in res.DeelLijst)
            {
                context.Entry(item).State = EntityState.Unchanged;
            }
            if(res.Activiteiten != null)
            foreach (var item in res.Activiteiten)
            {
                context.Entry(item).State = EntityState.Unchanged;
            }
            if(res.Eigenaar != null)
            context.Entry(res.Eigenaar).State = EntityState.Unchanged;

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return res;
        }
        public override void Delete(Boek entityToDelete)
        {
            base.Delete(entityToDelete);
            context.SaveChanges();

        }
        public override void Update(Boek entityToUpdate)
        {
            base.Update(entityToUpdate);
            context.SaveChanges();

        }


    }
}
