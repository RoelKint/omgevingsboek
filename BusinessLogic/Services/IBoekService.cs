using System;
namespace BusinessLogic.Services
{
    public interface IBoekService
    {
        void AddTagToPoi(int PoiId, int TagId);
        Models.OmgevingsBoek_Models.Uitnodiging CreateUitnodiging(string UitgenodigdDoorUserName, string EmailUitgenodigde);
        void DeleteActiviteit(Models.OmgevingsBoek_Models.Activiteit entityToDelete);
        void DeleteActiviteitSoft(Models.OmgevingsBoek_Models.Activiteit entityToDelete);
        void DeleteBoek(Models.OmgevingsBoek_Models.Boek boek);
        void DeleteBoekSoft(Models.OmgevingsBoek_Models.Boek boek);
        void DeletePoi(Models.OmgevingsBoek_Models.Poi poi);
        void DeletePoiSoft(Models.OmgevingsBoek_Models.Poi poi);
        void DeleteUserHard(Models.MVC_Models.ApplicationUser user);
        void EditBoek(Models.OmgevingsBoek_Models.Boek boek);
        Models.OmgevingsBoek_Models.Activiteit GetActiviteitById(int id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActiviteiten50FromSortNameAZ(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActiviteiten50FromSortNameZA(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActiviteiten50FromSortPoiAZ(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActiviteiten50FromSortPoiZA(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActiviteiten50FromSortUserAZ(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActiviteiten50FromSortUserZA(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getActiviteitenByPoiByUser50from(int from, string Owner, int PoiId);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActiviteitenList();
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getActiviteitenPerPoi(int id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getActiviteitenUserByUser50from(int from, string Owner, string Visitor);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActivitiesByUsername(string Username);
        Models.OmgevingsBoek_Models.Boek GetBoekByID(object id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> GetBoeken50FromSortNameAZ(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> GetBoeken50FromSortNameZA(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> GetBoeken50FromSortUserAZ(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> GetBoeken50FromSortUserZA(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> GetBoekenByUser(string username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> getBoekUserByUser50from(int from, string Owner, string Visitor);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> GetPoi50FromSortNameAZ(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> GetPoi50FromSortNameZA(int from);
        Models.OmgevingsBoek_Models.Poi GetPoiById(int Id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> GetPoiList();
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetSharedActivitiesByBookId(int BoekId, string Username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetSharedActivitiesByUsername(string Username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> GetSharedBoeken(string username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Tag> GetTagList();
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Tag> getTagsByPoi(int PoiId);
        Models.OmgevingsBoek_Models.Uitnodiging GetUitnodigingByKey(string key);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Uitnodiging> GetUitnodigingenAllByUser(string Username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Uitnodiging> GetUitnodigingenOpenByUser(string Username);
        Models.MVC_Models.ApplicationUser GetUser(string Username);
        Models.MVC_Models.ApplicationUser GetUserById(string UserId);
        System.Collections.Generic.List<Models.MVC_Models.ApplicationUser> GetUserNext50SortAZ(int from);
        System.Collections.Generic.List<Models.MVC_Models.ApplicationUser> GetUserNext50SortZA(int from);
        Models.OmgevingsBoek_Models.Boek InsertBoek(Models.OmgevingsBoek_Models.Boek boek);
        Models.OmgevingsBoek_Models.Poi InsertPoi(Models.OmgevingsBoek_Models.Poi poi);
        Models.OmgevingsBoek_Models.Tag InsertTag(string tag);
        bool IsActivityAccessibleByUser(int activiteitId, string Username);
        bool IsBoekAccessibleByUser(int BoekId, string Username);
        bool IsValidKey(string Key);
        bool SetUitnodigingGebruikt(int UitnodigingId, string GebruiktDoorUserName);
    }
}
