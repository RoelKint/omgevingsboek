using System;
namespace BusinessLogic.Repositories
{
    public interface IBoekOrderRepository
    {
        Models.OmgevingsBoek_Models.BoekOrder GetBoekOrder(string Username, int BoekId);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.BoekOrder> GetBoekOrderLijst(string Username, bool GedeeldeBoeken);
        Models.OmgevingsBoek_Models.BoekOrder Insert(Models.OmgevingsBoek_Models.BoekOrder entity);
        void Update(Models.OmgevingsBoek_Models.BoekOrder entityToUpdate);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.BoekOrder> UpdateLijst(System.Collections.Generic.List<Models.OmgevingsBoek_Models.BoekOrder> lijst);
    }
}
