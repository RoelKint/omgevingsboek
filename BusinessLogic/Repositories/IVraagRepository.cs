using System;
namespace BusinessLogic.Repositories
{
    public interface IVraagRepository
    {
        void Gelezen(int VraagId);
        Models.OmgevingsBoek_Models.Vraag GetByID(object id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Vraag> GetVragen();
        Models.OmgevingsBoek_Models.Vraag Insert(Models.OmgevingsBoek_Models.Vraag entity);
        void Opgelost(int VraagId);
    }
}
