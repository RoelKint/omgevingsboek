using System;
namespace BusinessLogic.Repositories
{
    public interface IBoekRepository
    {
        void Delete(Models.OmgevingsBoek_Models.Boek entityToDelete);
        void DeleteSoft(Models.OmgevingsBoek_Models.Boek entityToDelete);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> get50FromSortNameAZ(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> get50FromSortNameZA(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> get50FromSortUserAZ(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> get50FromSortUserZA(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> getBoekenByUser(string username);
        Models.OmgevingsBoek_Models.Boek GetByID(object id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> getSharedBoeken(string username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> getUserBoekByUser50from(int from, string Owner, string Visitor);
        Models.OmgevingsBoek_Models.Boek Insert(Models.OmgevingsBoek_Models.Boek entity);
        void Update(Models.OmgevingsBoek_Models.Boek entityToUpdate);
    }
}
