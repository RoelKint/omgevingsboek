using System;
namespace BusinessLogic.Repositories
{
    public interface IPoiRepository
    {
        void AddTag(int PoiId, int TagId, string UserName);
        System.Collections.Generic.IEnumerable<Models.OmgevingsBoek_Models.Poi> All();
        void Delete(Models.OmgevingsBoek_Models.Poi EntityToDelete);
        void DeleteSoft(Models.OmgevingsBoek_Models.Poi poi);
        void DeleteTag(int PoiTagId);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> get50FromSortAddressAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> get50FromSortAddressZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> get50FromSortDeletedAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> get50FromSortDeletedZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> get50FromSortEmailAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> get50FromSortEmailZA(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> get50FromSortNameAZ(int from, string search, bool DisplayDeleted);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> get50FromSortNameZA(int from, string search, bool DisplayDeleted);
        Models.OmgevingsBoek_Models.Poi GetByID(object id);
        Models.OmgevingsBoek_Models.Poi GetByIDAdmin(int id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.PoiTags> GetTags(int PoiId);
        Models.OmgevingsBoek_Models.Poi Insert(Models.OmgevingsBoek_Models.Poi entity);
        void Update(Models.OmgevingsBoek_Models.Poi entityToUpdate);
        void UpdateFoto(int PoiId, string foto);
    }
}
