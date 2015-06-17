﻿using System;
namespace BusinessLogic.Services
{
    public interface IBoekService
    {
        void AddFotoToActiviteit(int ActiviteitId, string Foto);
        void AddTagToPoi(int PoiId, int TagId, string UserName);
        void addUserToActiviteitShareList(int Id, string Username, bool IsGedeeld);
        void addUserToBoekShareList(int Id, string Username, bool IsGedeeld);
        Models.OmgevingsBoek_Models.Uitnodiging CreateUitnodiging(string UitgenodigdDoorUserName, string EmailUitgenodigde);
        void DeleteActiviteit(Models.OmgevingsBoek_Models.Activiteit entityToDelete);
        void DeleteActiviteitSoft(Models.OmgevingsBoek_Models.Activiteit entityToDelete);
        void DeleteBoek(Models.OmgevingsBoek_Models.Boek boek);
        void DeleteBoekSoft(Models.OmgevingsBoek_Models.Boek boek);
        void DeletePoi(Models.OmgevingsBoek_Models.Poi poi);
        void DeletePoiSoft(Models.OmgevingsBoek_Models.Poi poi);
        void DeletePoiTag(Models.OmgevingsBoek_Models.PoiTags poitag);
        void DeleteTagFromPoi(int PoiTagsId);
        void DeleteUserHard(Models.MVC_Models.ApplicationUser user);
        void DeleteUserSoft(Models.MVC_Models.ApplicationUser user);
        void EditBoek(Models.OmgevingsBoek_Models.Boek boek);
        Models.OmgevingsBoek_Models.Activiteit GetActiviteitById(int id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getActiviteiten50FromSortDeletedAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getActiviteiten50FromSortDeletedZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActiviteiten50FromSortNameAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActiviteiten50FromSortNameZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActiviteiten50FromSortPoiAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActiviteiten50FromSortPoiZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActiviteiten50FromSortUserAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActiviteiten50FromSortUserZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getActiviteitenByPoiByUser50from(int from, string Owner, int PoiId);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActiviteitenList();
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getActiviteitenPerPoi(int id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getActiviteitenUserByUser50from(int from, string Owner, string Visitor);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActivitiesByUsername(string Username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Benodigdheid> GetBenodigdhedenList();
        Models.OmgevingsBoek_Models.Boek GetBoekByID(object id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> GetBoeken50FromSortDeletedAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> GetBoeken50FromSortDeletedZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> GetBoeken50FromSortNameAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> GetBoeken50FromSortNameZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> GetBoeken50FromSortUserAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> GetBoeken50FromSortUserZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> GetBoekenByUser(string username);
        Models.OmgevingsBoek_Models.BoekOrder GetBoekOrder(string Username, int BoekId);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.BoekOrder> GetBoekOrderLijst(string Username, bool GedeeldeBoeken);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> getBoekUserByUser50from(int from, string Owner, string Visitor);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> getPoi50FromSortAddressAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> getPoi50FromSortAddressZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> getPoi50FromSortDeletedAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> getPoi50FromSortDeletedZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> getPoi50FromSortEmailAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> getPoi50FromSortEmailZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> GetPoi50FromSortNameAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> GetPoi50FromSortNameZA(int from, string search, bool DisplayDeleted);
        Models.OmgevingsBoek_Models.Poi GetPoiById(int Id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> GetPoiList();
        Models.OmgevingsBoek_Models.PoiTags getPoiTag(int TagId, int PoiId, string UserId);
        Models.OmgevingsBoek_Models.Route getRouteById(int Id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Route> getRoutesByBoek(int boekId);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetSharedActivitiesByBookId(int BoekId, string Username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetSharedActivitiesByUsername(string Username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> GetSharedBoeken(string username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Tag> GetTagList();
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.PoiTags> getTagsByPoi(int PoiId);
        Models.OmgevingsBoek_Models.Uitnodiging GetUitnodigingById(int id);
        Models.OmgevingsBoek_Models.Uitnodiging GetUitnodigingByKey(string key);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Uitnodiging> GetUitnodigingenAllByUser(string Username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Uitnodiging> GetUitnodigingenOpenByUser(string Username);
        Models.MVC_Models.ApplicationUser GetUser(string Username);
        Models.MVC_Models.ApplicationUser GetUserById(string UserId);
        System.Collections.Generic.List<Models.MVC_Models.ApplicationUser> GetUserNext30SortDeletedAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.MVC_Models.ApplicationUser> GetUserNext30SortDeletedZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.MVC_Models.ApplicationUser> GetUserNext30SortEmailAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.MVC_Models.ApplicationUser> GetUserNext30SortEmailZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.MVC_Models.ApplicationUser> GetUserNext30SortNaamAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.MVC_Models.ApplicationUser> GetUserNext30SortNaamZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.MVC_Models.ApplicationUser> GetUserNext30SortRoleAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.MVC_Models.ApplicationUser> GetUserNext30SortRoleZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.MVC_Models.ApplicationUser> GetUsers();
        bool HeeftEmailAlEenUitnodiging(string Email);
        Models.OmgevingsBoek_Models.BoekOrder Insert(Models.OmgevingsBoek_Models.BoekOrder entity);
        Models.OmgevingsBoek_Models.Activiteit InsertActiviteit(Models.OmgevingsBoek_Models.Activiteit activiteit);
        Models.OmgevingsBoek_Models.Benodigdheid InsertBenodigdheid(string benodigdheid);
        Models.OmgevingsBoek_Models.Boek InsertBoek(Models.OmgevingsBoek_Models.Boek boek);
        Models.OmgevingsBoek_Models.Poi InsertPoi(Models.OmgevingsBoek_Models.Poi poi);
        Models.OmgevingsBoek_Models.Route InsertRoute(Models.OmgevingsBoek_Models.Route entity);
        Models.OmgevingsBoek_Models.Tag InsertTag(string tag);
        bool IsActivityAccessibleByUser(int activiteitId, string Username);
        bool IsBoekAccessibleByUser(int BoekId, string Username);
        bool IsValidKey(string Key);
        bool SetUitnodigingGebruikt(int UitnodigingId, string GebruiktDoorUserName);
        Models.MVC_Models.ApplicationUser ToggleRole(string UserName);
        void Update(Models.OmgevingsBoek_Models.BoekOrder entityToUpdate);
        void UpdateActiviteit(Models.OmgevingsBoek_Models.Activiteit activiteit);
        void UpdateActiviteitFoto(int ActiviteitId, string foto);
        void UpdateBoekFoto(int BoekId, string afbeelding);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.BoekOrder> UpdateLijst(System.Collections.Generic.List<Models.OmgevingsBoek_Models.BoekOrder> lijst);
        void UpdatePoiFoto(int PoiId, string Afbeelding);
        Models.MVC_Models.ApplicationUser UpdateUserAfbeelding(Models.MVC_Models.ApplicationUser user);
    }
}
