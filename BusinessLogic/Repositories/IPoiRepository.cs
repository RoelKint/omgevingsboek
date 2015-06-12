using System;
namespace BusinessLogic.Repositories
{
    public interface IPoiRepository
    {
        void AddTag(int PoiId, int TagId);
        System.Collections.Generic.IEnumerable<Models.OmgevingsBoek_Models.Poi> All();
        void Delete(Models.OmgevingsBoek_Models.Poi EntityToDelete);
        void DeleteSoft(Models.OmgevingsBoek_Models.Poi poi);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> get50FromSortAddressAZ(int from, string search);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> get50FromSortAddressZA(int from, string search);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> get50FromSortEmailAZ(int from, string search);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> get50FromSortEmailZA(int from, string search);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> get50FromSortNameAZ(int from, string search);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> get50FromSortNameZA(int from, string search);
        Models.OmgevingsBoek_Models.Poi GetByID(object id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Tag> GetTags(int PoiId);
        Models.OmgevingsBoek_Models.Poi Insert(Models.OmgevingsBoek_Models.Poi entity);
        void Update(Models.OmgevingsBoek_Models.Poi entityToUpdate);
    }
}
