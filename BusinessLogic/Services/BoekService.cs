using BusinessLogic.Repositories;
using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class BoekService : BusinessLogic.Services.IBoekService 
    {
        private ITagRepository repoTag = null;
        private IActiviteitRepository repoActiviteit = null;
        private IBoekRepository repoBoek = null;
        private IPoiRepository repoPoi = null;
        private IUitnodigingRepository repoUitnodiging = null;
        private IBenodigdheidRepository repoBenodigdheid = null;

        public BoekService(
            ITagRepository repoTag,
            IActiviteitRepository repoActiviteit,
            IBoekRepository repoBoek,
            IPoiRepository repoPoi,
            IUitnodigingRepository repoUitnodiging,
            IBenodigdheidRepository repoBenodigdheid
            )
        {
            this.repoActiviteit = repoActiviteit;
            this.repoBoek = repoBoek;
            this.repoTag = repoTag;
            this.repoPoi = repoPoi;
            this.repoUitnodiging = repoUitnodiging;
            this.repoBenodigdheid = repoBenodigdheid;
        }

        #region Activiteiten

        public bool IsActivityAccessibleByUser(int activiteitId, string Username)
        {
            return repoActiviteit.IsActivityAccessibleByUser(activiteitId, Username);
        }

        public List<Activiteit> GetActiviteitenList()
        {
            return repoActiviteit.All().ToList();
        }
        public Activiteit GetActiviteitById(int id)
        {
            return repoActiviteit.GetByID(id);
        }
        public List<Activiteit> GetActivitiesByUsername(string Username)
        {
            return repoActiviteit.getActivitiesByUsername(Username);
        }
        public List<Activiteit> GetSharedActivitiesByUsername(string Username)
        {
            return repoActiviteit.getSharedActivitiesByUsername(Username);
        }
        public List<Activiteit> GetSharedActivitiesByBookId(int BoekId, string Username)
        {
            return repoActiviteit.getSharedActivitiesByBookId(BoekId, Username);
        }
        public List<Activiteit> getActiviteitenPerPoi(int id)
        {
            return repoActiviteit.getActiviteitenPerPoi(id);
        }

        public List<Activiteit> GetActiviteiten50FromSortNameAZ(int from)
        {
            return repoActiviteit.get50FromSortNameAZ(from);
        }
        public List<Activiteit> GetActiviteiten50FromSortNameZA(int from)
        {
            return repoActiviteit.get50FromSortNameZA(from);
        }
        public List<Activiteit> GetActiviteiten50FromSortUserAZ(int from)
        {
            return repoActiviteit.get50FromSortUserAZ(from);
        }
        public List<Activiteit> GetActiviteiten50FromSortUserZA(int from)
        {
            return repoActiviteit.get50FromSortUserZA(from);
        }
        public List<Activiteit> GetActiviteiten50FromSortPoiAZ(int from)
        {
            return repoActiviteit.get50FromSortPoiAZ(from);
        }
        public List<Activiteit> GetActiviteiten50FromSortPoiZA(int from)
        {
            return repoActiviteit.get50FromSortPoiZA(from);
        }

        public List<Activiteit> getActiviteitenUserByUser50from(int from, String Owner, String Visitor)
        {
            return repoActiviteit.getUserActiviteitenByUser50from(from, Owner, Visitor);
        }
        public void DeleteActiviteitSoft(Activiteit entityToDelete)
        {
            repoActiviteit.DeleteSoft(entityToDelete);
        }
        public void DeleteActiviteit(Activiteit entityToDelete)
        {
            repoActiviteit.Delete(entityToDelete);
        }

        public List<Activiteit> getActiviteitenByPoiByUser50from(int from, String Owner, int PoiId)
        {
            return repoActiviteit.getActiviteitenByPoiByUser50from(from, Owner, PoiId);
        }
        public Activiteit InsertActiviteit(Activiteit activiteit)
        {
            return repoActiviteit.Insert(activiteit);
        }

        #endregion



        #region Boeken
        public bool IsBoekAccessibleByUser(int BoekId, string Username)
        {
            return repoBoek.IsBoekAccessibleByUser(BoekId, Username);
        }
        public Boek InsertBoek(Boek boek)
        {
            return repoBoek.Insert(boek);
        }
        public void EditBoek(Boek boek)
        {
            repoBoek.Update(boek);
        }
        public void DeleteBoek(Boek boek)
        {
            repoBoek.Delete(boek);
        }
        public void DeleteBoekSoft(Boek boek)
        {
            repoBoek.DeleteSoft(boek);
        }
        public Boek GetBoekByID(object id)
        {
            return repoBoek.GetByID(id);
        }
        public List<Boek> GetBoekenByUser(string username)
        {
            return repoBoek.getBoekenByUser(username);
        }
        //sharedboeken omvatten niet de eigen boeken.
        public List<Boek> GetSharedBoeken(string username)
        {
            return repoBoek.getSharedBoeken(username);
        }

        public List<Boek> GetBoeken50FromSortNameAZ(int from)
        {
            return repoBoek.get50FromSortNameAZ(from);
        }
        public List<Boek> GetBoeken50FromSortNameZA(int from)
        {
            return repoBoek.get50FromSortNameZA(from);
        }
        public List<Boek> GetBoeken50FromSortUserAZ(int from)
        {
            return repoBoek.get50FromSortUserAZ(from);
        }
        public List<Boek> GetBoeken50FromSortUserZA(int from)
        {
            return repoBoek.get50FromSortUserZA(from);
        }
        public List<Boek> getBoekUserByUser50from(int from, String Owner, String Visitor)
        {
            return repoBoek.getUserBoekByUser50from(from, Owner, Visitor);
        }


        #endregion



        #region Poi

        public List<Poi> GetPoi50FromSortNameAZ(int from)
        {
            return repoPoi.get50FromSortNameAZ(from);
        }
        public List<Poi> GetPoi50FromSortNameZA(int from)
        {
            return repoPoi.get50FromSortNameZA(from);
        }
        
        public List<Poi> GetPoiList()
        {
            return repoPoi.All().ToList();
        }
        public Poi InsertPoi(Poi poi)
        {
            return repoPoi.Insert(poi);
        }

        public void DeletePoi(Poi poi)
        {
            repoPoi.Delete(poi);
        }
        public void DeletePoiSoft(Poi poi)
        {
            repoPoi.DeleteSoft(poi);
        }
        public Poi GetPoiById(int Id)
        {
            return repoPoi.GetByID(Id);
        }
        public void AddTagToPoi(int PoiId, int TagId)
        {
            repoPoi.AddTag(PoiId, TagId);
        }
        public List<Tag> getTagsByPoi(int PoiId)
        {
            return repoPoi.GetTags(PoiId);
        }

        #endregion



        #region Tags

        public List<Tag> GetTagList()
        {
            return repoTag.All().ToList();
        }
        public Tag InsertTag(string tag)
        {
            return repoTag.Insert(tag);
        }

        #endregion



        #region Benodigdheden

        public List<Benodigdheid> GetBenodigdhedenList()
        {
            return repoBenodigdheid.All().ToList() ;
        }
        public Benodigdheid InsertBenodigdheid(string benodigdheid)
        {
            return repoBenodigdheid.Insert(benodigdheid);
        }

        #endregion



        #region Users

        public ApplicationUser GetUser(String Username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Users.Select(i => i).Where(i => i.UserName == Username).Single();
            }
        }
        public ApplicationUser GetUserById(String UserId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Users.Select(i => i).Where(i => i.Id == UserId).Single();
            }
        }

        public List<ApplicationUser> GetUserNext50SortAZ(int from)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Users.OrderBy(i => i.UserName).Skip(from).Take(30).ToList();
            }
        }

        public List<ApplicationUser> GetUserNext50SortZA(int from)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Users.OrderByDescending(i => i.UserName).Skip(from).Take(30).ToList();
            }
        }
        // TODO: soft delete 
        public void DeleteUserHard(ApplicationUser user)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Users.Remove(user);
            }
        }

        public ApplicationUser UpdateUserAfbeelding(ApplicationUser user)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ApplicationUser appuser = context.Users.Where(u => u.Id == user.Id).FirstOrDefault();
                appuser.Afbeelding = user.Afbeelding;
                context.SaveChanges();
                return appuser;
            }
        }


        #endregion



        #region uitnodigingen

        public Uitnodiging CreateUitnodiging(string UitgenodigdDoorUserName, string EmailUitgenodigde)
        {
            return repoUitnodiging.Create(UitgenodigdDoorUserName, EmailUitgenodigde);

        }
        public bool IsValidKey(string Key)
        {
            return repoUitnodiging.IsValidKey(Key);
        }
        public Uitnodiging GetUitnodigingByKey(string key)
        {
            return repoUitnodiging.GetUitnodigingByKey(key);
        }

        public List<Uitnodiging> GetUitnodigingenOpenByUser(string Username)
        {
            return repoUitnodiging.GetUitnodigingenOpenByUser(Username);
        }
        public List<Uitnodiging> GetUitnodigingenAllByUser(string Username)
        {
            return repoUitnodiging.GetUitnodigingenAllByUser(Username);
        }
        
        public bool SetUitnodigingGebruikt(int UitnodigingId, string GebruiktDoorUserName)
        {
            return repoUitnodiging.SetUitnodigingGebruikt(UitnodigingId, GebruiktDoorUserName);

        }
        public bool HeeftEmailAlEenUitnodiging(string Email)
        {
           return repoUitnodiging.HeeftEmailAlEenUitnodiging(Email);
        }

        public Uitnodiging GetUitnodigingById(int id)
        {
            return repoUitnodiging.GetByID(id);
        }

        #endregion






    }
}
