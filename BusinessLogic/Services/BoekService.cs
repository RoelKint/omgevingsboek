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

        public BoekService(
            ITagRepository repoTag,
            IActiviteitRepository repoActiviteit,
            IBoekRepository repoBoek,
            IPoiRepository repoPoi
            )
        {
            this.repoActiviteit = repoActiviteit;
            this.repoBoek = repoBoek;
            this.repoTag = repoTag;
            this.repoPoi = repoPoi;
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

        #endregion



        #region Tags

        public List<Tag> GetTagList()
        {
            return repoTag.All().ToList();
        }
        public Tag InsertTag(Tag tag)
        {
            return repoTag.Insert(tag);
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
                return context.Users.OrderBy(i => i.UserName).Skip(from).Take(50).ToList();
            }
        }

        public List<ApplicationUser> GetUserNext50SortZA(int from)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Users.OrderByDescending(i => i.UserName).Skip(from).Take(50).ToList();
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

        #endregion


        
        
        
        
        
        
    }
}
