using BusinessLogic.Repositories;
using Models.MVC_Models;
using Models.OmgevingsBoek_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
        private IGenericRepository<PoiTags> repoPoiTags = null;
        private IBoekOrderRepository repoBoekOrder = null;
        private IRouteRepository repoRoute = null;

        public BoekService(
            ITagRepository repoTag,
            IActiviteitRepository repoActiviteit,
            IBoekRepository repoBoek,
            IPoiRepository repoPoi,
            IUitnodigingRepository repoUitnodiging,
            IBenodigdheidRepository repoBenodigdheid,
            IGenericRepository<PoiTags> repoPoiTags,
            IBoekOrderRepository repoBoekOrder,
            IRouteRepository repoRoute
            )
        {
            this.repoActiviteit = repoActiviteit;
            this.repoBoek = repoBoek;
            this.repoTag = repoTag;
            this.repoPoi = repoPoi;
            this.repoUitnodiging = repoUitnodiging;
            this.repoBenodigdheid = repoBenodigdheid;
            this.repoPoiTags = repoPoiTags;
            this.repoBoekOrder = repoBoekOrder;
            this.repoRoute = repoRoute;
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

        public List<Activiteit> GetActiviteiten50FromSortNameAZ(int from, string search, bool DisplayDeleted)
        {
            return repoActiviteit.get50FromSortNameAZ(from,search,DisplayDeleted);
        }
        public List<Activiteit> GetActiviteiten50FromSortNameZA(int from, string search, bool DisplayDeleted)
        {
            return repoActiviteit.get50FromSortNameZA(from, search, DisplayDeleted);
        }
        public List<Activiteit> GetActiviteiten50FromSortUserAZ(int from, string search, bool DisplayDeleted)
        {
            return repoActiviteit.get50FromSortUserAZ(from, search, DisplayDeleted);
        }
        public List<Activiteit> GetActiviteiten50FromSortUserZA(int from, string search, bool DisplayDeleted)
        {
            return repoActiviteit.get50FromSortUserZA(from, search, DisplayDeleted);
        }
        public List<Activiteit> GetActiviteiten50FromSortPoiAZ(int from, string search, bool DisplayDeleted)
        {
            return repoActiviteit.get50FromSortPoiAZ(from, search, DisplayDeleted);
        }
        public List<Activiteit> GetActiviteiten50FromSortPoiZA(int from, string search, bool DisplayDeleted)
        {
            return repoActiviteit.get50FromSortPoiZA(from, search, DisplayDeleted);
        }
        public List<Activiteit> getActiviteiten50FromSortDeletedAZ(int from, string search, bool DisplayDeleted)
        {
            return repoActiviteit.get50FromSortDeletedAZ(from, search, DisplayDeleted);
        }
        public List<Activiteit> getActiviteiten50FromSortDeletedZA(int from, string search, bool DisplayDeleted)
        {
            return repoActiviteit.get50FromSortDeletedZA(from, search, DisplayDeleted);
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
        public void AddFotoToActiviteit(int ActiviteitId, string Foto)
        {
            repoActiviteit.AddFotoToActiviteit(ActiviteitId, Foto);
        }

        public void UpdateActiviteitFoto(int ActiviteitId, string foto)
        {
            repoActiviteit.UpdateActiviteitFoto(ActiviteitId, foto);
        }
        public void addUserToActiviteitShareList(int Id, string Username, bool IsGedeeld)
        {
            repoActiviteit.addUserToShareList(Id, Username, IsGedeeld);
        }
        public void UpdateActiviteit(Activiteit activiteit)
        {
            repoActiviteit.UpdateActiviteit(activiteit);
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

        public List<Boek> GetBoeken50FromSortNameAZ(int from, string search, bool DisplayDeleted)
        {
            return repoBoek.get50FromSortNameAZ(from, search, DisplayDeleted);
        }
        public List<Boek> GetBoeken50FromSortNameZA(int from, string search, bool DisplayDeleted)
        {
            return repoBoek.get50FromSortNameZA(from, search, DisplayDeleted);
        }
        public List<Boek> GetBoeken50FromSortUserAZ(int from, string search, bool DisplayDeleted)
        {
            return repoBoek.get50FromSortUserAZ(from, search, DisplayDeleted);
        }
        public List<Boek> GetBoeken50FromSortUserZA(int from, string search, bool DisplayDeleted)
        {
            return repoBoek.get50FromSortUserZA(from, search, DisplayDeleted);
        }
        public List<Boek> getBoekUserByUser50from(int from, String Owner, String Visitor)
        {
            return repoBoek.getUserBoekByUser50from(from, Owner, Visitor);
        }
        public List<Boek> GetBoeken50FromSortDeletedAZ(int from, string search, bool DisplayDeleted)
        {
            return repoBoek.get50FromSortDeletedAZ(from, search, DisplayDeleted);
        }
        public List<Boek> GetBoeken50FromSortDeletedZA(int from, string search, bool DisplayDeleted)
        {
            return repoBoek.get50FromSortDeletedZA(from, search, DisplayDeleted);
        }

        public void UpdateBoekFoto(int BoekId, string afbeelding)
        {
            repoBoek.UpdateFoto(BoekId, afbeelding);
        }
        public void addUserToBoekShareList(int Id, string Username, bool IsGedeeld)
        {
            repoBoek.addUserToShareList(Id, Username, IsGedeeld);
        }

        #endregion




        #region BoekOrder



        public List<BoekOrder> GetBoekOrderLijst(string Username, bool GedeeldeBoeken)
        {
            return repoBoekOrder.GetBoekOrderLijst(Username, GedeeldeBoeken);

        }
        public BoekOrder GetBoekOrder(string Username, int BoekId)
        {
            return repoBoekOrder.GetBoekOrder(Username, BoekId);
        }
        public List<BoekOrder> UpdateLijst(List<BoekOrder> lijst)
        {
            return repoBoekOrder.UpdateLijst(lijst);
        }
        public void Update(BoekOrder entityToUpdate)
        {
            repoBoekOrder.Update(entityToUpdate);
        }

        public BoekOrder Insert(BoekOrder entity)
        {
            return repoBoekOrder.Insert(entity);
        }



        #endregion




        #region Poi

        public List<Poi> GetPoi50FromSortNameAZ(int from, string search, bool DisplayDeleted)
        {
            return repoPoi.get50FromSortNameAZ(from, search, DisplayDeleted);
        }
        public List<Poi> GetPoi50FromSortNameZA(int from, string search, bool DisplayDeleted)
        {
            return repoPoi.get50FromSortNameZA(from, search, DisplayDeleted);
        }
        public List<Poi> getPoi50FromSortEmailAZ(int from, string search, bool DisplayDeleted)
        {
            return repoPoi.get50FromSortEmailAZ(from, search, DisplayDeleted);
        }
        public List<Poi> getPoi50FromSortEmailZA(int from, string search, bool DisplayDeleted)
        {
            return repoPoi.get50FromSortEmailZA(from, search, DisplayDeleted);

        }
        public List<Poi> getPoi50FromSortAddressAZ(int from, string search, bool DisplayDeleted)
        {
            return repoPoi.get50FromSortAddressAZ(from, search, DisplayDeleted);

        }
        public List<Poi> getPoi50FromSortAddressZA(int from, string search, bool DisplayDeleted)
        {
            return repoPoi.get50FromSortAddressZA(from, search, DisplayDeleted);

        }
        public List<Poi> getPoi50FromSortDeletedAZ(int from, string search, bool DisplayDeleted)
        {
            return repoPoi.get50FromSortDeletedAZ(from, search, DisplayDeleted);

        }
        public List<Poi> getPoi50FromSortDeletedZA(int from, string search, bool DisplayDeleted)
        {
            return repoPoi.get50FromSortDeletedZA(from, search, DisplayDeleted);

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
        public void AddTagToPoi(int PoiId, int TagId,string UserName)
        {
            repoPoi.AddTag(PoiId, TagId,UserName);
        }
        public List<PoiTags> getTagsByPoi(int PoiId)
        {
            return repoPoi.GetTags(PoiId);
        }
        public void DeleteTagFromPoi(int PoiTagsId)
        {
            repoPoi.DeleteTag(PoiTagsId);
        }

        public void UpdatePoiFoto(int PoiId, string Afbeelding)
        {
            repoPoi.UpdateFoto(PoiId, Afbeelding);
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



        #region PoiTags

        public PoiTags getPoiTag(int TagId,int PoiId, string UserId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.PoiTags.Where(p => p.EigenaarId == UserId).Where(p => p.PoiId == PoiId).Where(p => p.TagId == TagId).FirstOrDefault();
            }
        }
        public void DeletePoiTag(PoiTags poitag)
        {
            repoPoiTags.Delete(poitag);
            repoPoiTags.SaveChanges();
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

        public List<ApplicationUser> GetUsers()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.Users.ToList();
            }
            
        }

        public ApplicationUser GetUser(String Username)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Users.Select(i => i).Where(i => i.UserName == Username).Include(i => i.Roles).Single();
            }
        }
        public ApplicationUser GetUserById(String UserId)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return context.Users.Select(i => i).Where(i => i.Id == UserId).Single();
            }
        }

        public List<ApplicationUser> GetUserNext30SortEmailAZ(int from,string search, bool DisplayDeleted)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.Users.OrderBy(i => i.UserName).Include(u => u.Roles).Where(u => u.Deleted == false || u.Deleted == DisplayDeleted).Where(u => u.UserName.Contains(search) || u.Voornaam.Contains(search) || u.Naam.Contains(search)).Skip(from).Take(30).ToList();
            }
        }
        public List<ApplicationUser> GetUserNext30SortEmailZA(int from, string search, bool DisplayDeleted)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.Users.OrderByDescending(i => i.UserName).Where(u => u.Deleted == false || u.Deleted == DisplayDeleted).Include(u => u.Roles).Where(u => u.UserName.Contains(search) || u.Voornaam.Contains(search) || u.Naam.Contains(search)).Skip(from).Take(30).ToList();
            }
        }
        public List<ApplicationUser> GetUserNext30SortNaamAZ(int from, string search, bool DisplayDeleted)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.Users.OrderBy(i => i.Voornaam).Include(u => u.Roles).Where(u => u.Deleted == false || u.Deleted == DisplayDeleted).Where(u => u.UserName.Contains(search) || u.Voornaam.Contains(search) || u.Naam.Contains(search)).Skip(from).Take(30).ToList();
            }
        }
        public List<ApplicationUser> GetUserNext30SortNaamZA(int from, string search, bool DisplayDeleted)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.Users.OrderByDescending(i => i.Voornaam).Where(u => u.Deleted == false || u.Deleted == DisplayDeleted).Include(u => u.Roles).Where(u => u.UserName.Contains(search) || u.Voornaam.Contains(search) || u.Naam.Contains(search)).Skip(from).Take(30).ToList();
            }
        }
        public List<ApplicationUser> GetUserNext30SortDeletedAZ(int from, string search, bool DisplayDeleted)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.Users.OrderBy(i => i.Deleted).Include(u => u.Roles).Where(u => u.Deleted == false || u.Deleted == DisplayDeleted).Where(u => u.UserName.Contains(search) || u.Voornaam.Contains(search) || u.Naam.Contains(search)).Skip(from).Take(30).ToList();
            }
        }
        public List<ApplicationUser> GetUserNext30SortDeletedZA(int from, string search, bool DisplayDeleted)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.Users.OrderByDescending(i => i.Deleted).Where(u => u.Deleted == false || u.Deleted == DisplayDeleted).Include(u => u.Roles).Where(u => u.UserName.Contains(search) || u.Voornaam.Contains(search) || u.Naam.Contains(search)).Skip(from).Take(30).ToList();
            }
        }
        public List<ApplicationUser> GetUserNext30SortRoleAZ(int from, string search, bool DisplayDeleted)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.Users.OrderBy(i => i.Roles.FirstOrDefault()).Include(u => u.Roles).Where(u => u.Deleted == false || u.Deleted == DisplayDeleted).Where(u => u.UserName.Contains(search) || u.Voornaam.Contains(search) || u.Naam.Contains(search)).Skip(from).Take(30).ToList();
            }
        }
        public List<ApplicationUser> GetUserNext30SortRoleZA(int from, string search, bool DisplayDeleted)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.Users.OrderByDescending(i => i.Roles.FirstOrDefault()).Where(u => u.Deleted == false || u.Deleted == DisplayDeleted).Include(u => u.Roles).Where(u => u.UserName.Contains(search) || u.Voornaam.Contains(search) || u.Naam.Contains(search)).Skip(from).Take(30).ToList();
            }
        }
        // TODO: soft delete 
        public void DeleteUserSoft(ApplicationUser user)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Users.Where(i => i.Id == user.Id).FirstOrDefault().Deleted = true;
                context.SaveChanges();
            }
        }
        public void DeleteUserHard(ApplicationUser user)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Users.Remove(user);
                context.SaveChanges();
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

        public ApplicationUser ToggleRole(string UserName)
        {
            ApplicationUser user = GetUser(UserName);
            UserManager<ApplicationUser> um = new UserManager<ApplicationUser>(new UserStore<Models.MVC_Models.ApplicationUser>(new ApplicationDbContext()));
            if (um.IsInRole(user.Id, "User")){
                um.AddToRole(user.Id,"Administrator");
                um.RemoveFromRole(user.Id, "User");
            }
            else if (um.IsInRole(user.Id, "Administrator")){
                um.AddToRole(user.Id,"User");
                um.RemoveFromRole(user.Id, "Administrator");
            }
            return user;
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


        #region routes

        public List<Route> getRoutesByBoek(int boekId)
        {
            return repoRoute.getRoutesByBoek(boekId);
        }
        public Route InsertRoute(Route entity)
        {
            return repoRoute.Insert(entity);
        }
        public Route getRouteById(int Id)
        {
            return repoRoute.GetByID(Id);
        }

        #endregion



    }
}
