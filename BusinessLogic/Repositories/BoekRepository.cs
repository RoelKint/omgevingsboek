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
    public class BoekRepository : GenericRepository<Boek>, BusinessLogic.Repositories.IBoekRepository
    {
        public BoekRepository(ApplicationDbContext context)
            : base(context)
        {

        }
        public BoekRepository()
            : base(new ApplicationDbContext())
        {
            context.Configuration.LazyLoadingEnabled = false;
        }

        public bool IsBoekAccessibleByUser(int BoekId, string Username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ApplicationUser user = context.Users.Where(u => u.UserName == Username).FirstOrDefault();
                Boek b = context.Boeken.Where(a => a.Id == BoekId).FirstOrDefault();
                if (b == null) return false;
                return b.DeelLijst.Contains(user);

            }
        }

        public override Boek GetByID(object id)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Boeken
                .Include(b => b.DeelLijst)
                .Include(b => b.Activiteiten/*.Select(i => i.)*/)
                .Include(a => a.Eigenaar)
                .Where(b => !b.IsDeleted)
                .Where(b => b.Id == (int)id)
                .SingleOrDefault();
        }
        public List<Boek> getBoekenByUser(string username)
        {
            return (from b in context.Boeken.Include(b => b.DeelLijst).Include(b => b.Activiteiten) where !b.IsDeleted where b.Eigenaar.UserName == username select b).ToList();
        }
        //sharedboeken omvatten niet de eigen boeken.
        public List<Boek> getSharedBoeken(string username)
        {
            return (from b in context.Boeken.Include(b => b.DeelLijst).Include(b => b.Activiteiten) where !b.IsDeleted where b.DeelLijst.Contains(context.Users.Select(i=>i).Where(i => i.UserName == username).FirstOrDefault()) where b.Eigenaar.UserName != username select b).ToList();
        }
        public override Boek Insert(Boek entity)
        {
            Boek res = entity;
            res.DeelLijst = new List<ApplicationUser>();
            res.DeelLijst.Add(context.Users.Where(u => u.Id == entity.EigenaarId).FirstOrDefault());
            res = base.Insert(entity);

            int previndex;
            List<BoekOrder> l = context.BoekOrder.Where(i => i.EigenaarId == entity.EigenaarId).Where(i => i.IsSharedLijst == false).ToList();
            if (l.Count != 0)
                previndex = context.BoekOrder.Where(i => i.EigenaarId == entity.EigenaarId).Where(i => i.IsSharedLijst == false).Max(i => i.Index);
            else
                previndex = -1;
            context.BoekOrder.Add(new BoekOrder()
            {
                BoekId = res.Id,
                EigenaarId = entity.EigenaarId,
                Index = (previndex + 1),
                IsSharedLijst = false
            });
            
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

            foreach (BoekOrder bo in context.BoekOrder.Where(b => b.BoekId == entityToDelete.Id).ToList())
            {
                context.BoekOrder.Remove(bo);

            }
            base.Delete(entityToDelete);
            
            context.SaveChanges();

        }
        public override void Update(Boek entityToUpdate)
        {
            base.Update(entityToUpdate);
            context.SaveChanges();

        }

        public List<Boek> get50FromSortNameAZ(int from, string search)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Boeken.Include(b => b.Eigenaar).Include(b => b.Activiteiten).Where(i => !i.IsDeleted).Where(b => b.Naam.Contains(search) || b.Eigenaar.UserName.Contains(search)).OrderBy(i => i.Naam).Skip(from).Take(30).ToList();
        }
        public List<Boek> get50FromSortNameZA(int from, string search)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Boeken.Include(b => b.Eigenaar).Include(b => b.Activiteiten).Where(i => !i.IsDeleted).Where(b => b.Naam.Contains(search) || b.Eigenaar.UserName.Contains(search)).OrderByDescending(i => i.Naam).Skip(from).Take(30).ToList();
        }
        public List<Boek> get50FromSortUserAZ(int from, string search)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Boeken.Include(b => b.Eigenaar).Include(b => b.Activiteiten).Where(i => !i.IsDeleted).Where(b => b.Naam.Contains(search) || b.Eigenaar.UserName.Contains(search)).OrderBy(i => i.Eigenaar.UserName).Skip(from).Take(30).ToList();
        }
        public List<Boek> get50FromSortUserZA(int from, string search)
        {
            context.Configuration.LazyLoadingEnabled = false;
            return this.context.Boeken.Include(b => b.Eigenaar).Include(b => b.Activiteiten).Where(i => !i.IsDeleted).Where(b => b.Naam.Contains(search) || b.Eigenaar.UserName.Contains(search)).OrderByDescending(i => i.Eigenaar.UserName).Skip(from).Take(30).ToList();
        }

        public void DeleteSoft(Boek entityToDelete)
        {
            foreach (BoekOrder bo in context.BoekOrder.Where(b => b.BoekId == entityToDelete.Id).ToList())
            {
                context.BoekOrder.Remove(bo);

            }
            entityToDelete.IsDeleted = true;
            Update(entityToDelete);
            context.SaveChanges();

        }

        public List<Boek> getUserBoekByUser50from(int from, String Owner, String Visitor)
        {
            return this.context.Boeken.Where(i => !i.IsDeleted).Where(i => i.Eigenaar.UserName == Owner).Where(i => i.DeelLijst.Contains(context.Users.Select(u => u).Where(u => u.UserName == Visitor).FirstOrDefault())).OrderBy(i => i.Naam).Skip(from).Take(50).ToList();
        }
        public void UpdateFoto(int BoekId, string foto)
        {
            Boek b = GetByID(BoekId);
            b.Afbeelding = foto;
            Update(b);
        }
        public void addUserToShareList(int Id, string Username)
        {
            Boek b = GetByID(Id);
            ApplicationUser user = context.Users.Where(u => u.UserName == Username).FirstOrDefault();
            b.DeelLijst.Add(user);

            int previndex;
            List<BoekOrder> l = context.BoekOrder.Where(i => i.EigenaarId == user.Id).Where(i => i.IsSharedLijst == true).ToList();
            if (l.Count != 0 )
                previndex = context.BoekOrder.Where(i => i.EigenaarId == user.Id).Where(i => i.IsSharedLijst == true).Max(i => i.Index);
            else
                previndex = -1;
            context.BoekOrder.Add(new BoekOrder()
            {
                BoekId = b.Id,
                EigenaarId = user.Id,
                Index = (previndex + 1),
                IsSharedLijst = true
            });
            
            Update(b);
        }

    }
}
