using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories
{
    public class UitnodigingRepository : GenericRepository<Uitnodiging>, BusinessLogic.Repositories.IUitnodigingRepository
    {
        public UitnodigingRepository(ApplicationDbContext context)
            : base(context)
        {
            context.Configuration.LazyLoadingEnabled = false;
        }
        public UitnodigingRepository()
            : base(new ApplicationDbContext())
        {

        }
        public Uitnodiging Create(string UitgenodigdDoorUserName,string EmailUitgenodigde)
        {
            Guid g = Guid.NewGuid();
            Uitnodiging res = new Uitnodiging();
            res.Eigenaar = context.Users.Where(u => u.UserName == UitgenodigdDoorUserName).FirstOrDefault();
            res.Key = g.ToString();
            res.EmailUitgenodigde = EmailUitgenodigde;

            res = context.Uitnodigingen.Add(res);
            context.SaveChanges();
            return res;

        }
        public bool IsValidKey(string Key)
        {
            Uitnodiging u = GetUitnodigingByKey(Key);
            if (u == null) return false;
            if (!u.Gebruikt) return true;
            return false;
        }
        public Uitnodiging GetUitnodigingByKey(string key)
        {
            return context.Uitnodigingen.Where(u => u.Key == key).SingleOrDefault();
        }

        public List<Uitnodiging> GetUitnodigingenOpenByUser(string Username)
        {
            return context.Uitnodigingen.Where(u => u.Gebruikt == false).Where(u => u.EigenaarId == context.Users.Where(i => i.UserName == Username).FirstOrDefault().Id).ToList();
        }
        public List<Uitnodiging> GetUitnodigingenAllByUser(string Username)
        {
            return context.Uitnodigingen.Where(u => u.EigenaarId == context.Users.Where(i => i.UserName == Username).FirstOrDefault().Id).ToList();
        }
        public override void Update(Uitnodiging entityToUpdate)
        {
            base.Update(entityToUpdate);
            context.SaveChanges();
        }
        public bool SetUitnodigingGebruikt(int UitnodigingId, string GebruiktDoorUserName)
        {
            Uitnodiging res = context.Uitnodigingen.Where(u => u.Id == UitnodigingId).FirstOrDefault();
            ApplicationUser user = context.Users.Where(u => u.UserName == GebruiktDoorUserName).FirstOrDefault();
            if (res == null) return false;
            if (user == null) return false;
            if (context.Uitnodigingen.Where(u => u.GebruiktDoorId == user.Id).FirstOrDefault() != null) return false;
            res.Gebruikt = true;
            res.GebruiktDoorId = user.Id;

            Update(res);
            return true;

        }
        public bool HeeftEmailAlEenUitnodiging(string Email)
        {
            return (context.Uitnodigingen.Where(u => u.EmailUitgenodigde == Email).FirstOrDefault() != null);
        }



    }
}
