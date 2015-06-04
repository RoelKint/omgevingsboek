using System;
namespace BusinessLogic.Repositories
{
    public interface IBoekRepository
    {
        void Delete(Models.OmgevingsBoek_Models.Boek entityToDelete);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> get50();
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> get50From(int from);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> getBoekenByUser(string username);
        Models.OmgevingsBoek_Models.Boek GetByID(object id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> getSharedBoeken(string username);
        Models.OmgevingsBoek_Models.Boek Insert(Models.OmgevingsBoek_Models.Boek entity);
        void Update(Models.OmgevingsBoek_Models.Boek entityToUpdate);
    }
}
