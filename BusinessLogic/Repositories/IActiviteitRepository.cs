using System;
namespace BusinessLogic.Repositories
{
    public interface IActiviteitRepository
    {
        void AddFotoToActiviteit(int ActiviteitId, string Foto);
        void addUserToShareList(int Id, string Username, bool IsGedeeld);
        System.Collections.Generic.IEnumerable<Models.OmgevingsBoek_Models.Activiteit> All();
        void Delete(Models.OmgevingsBoek_Models.Activiteit id);
        void DeleteSoft(Models.OmgevingsBoek_Models.Activiteit entityToDelete);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> get50FromSortDeletedAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> get50FromSortDeletedZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> get50FromSortNameAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> get50FromSortNameZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> get50FromSortPoiAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> get50FromSortPoiZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> get50FromSortUserAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> get50FromSortUserZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getActiviteitenByPoiByUser50from(int from, string Owner, int PoiId);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getActiviteitenPerPoi(int id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getActivitiesByUsername(string Username);
        Models.OmgevingsBoek_Models.Activiteit GetByID(object id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getSharedActivitiesByBookId(int BoekId, string Username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getSharedActivitiesByUsername(string Username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getUserActiviteitenByUser50from(int from, string Owner, string Visitor);
        Models.OmgevingsBoek_Models.Activiteit Insert(Models.OmgevingsBoek_Models.Activiteit entity);
        bool IsActivityAccessibleByUser(int activiteitId, string Username);
        void UpdateActiviteit(Models.OmgevingsBoek_Models.Activiteit activiteit);
        void UpdateActiviteitFoto(int ActiviteitId, string foto);
    }
}
