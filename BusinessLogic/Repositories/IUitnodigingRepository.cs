using System;
namespace BusinessLogic.Repositories
{
    public interface IUitnodigingRepository 
    {
        Models.OmgevingsBoek_Models.Uitnodiging Create(string UitgenodigdDoorUserName, string EmailUitgenodigde);
        Models.OmgevingsBoek_Models.Uitnodiging GetByID(object id);
        Models.OmgevingsBoek_Models.Uitnodiging GetUitnodigingByKey(string key);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Uitnodiging> GetUitnodigingenAllByUser(string Username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Uitnodiging> GetUitnodigingenOpenByUser(string Username);
        bool HeeftEmailAlEenUitnodiging(string Email);
        bool IsValidKey(string Key);
        bool SetUitnodigingGebruikt(int UitnodigingId, string GebruiktDoorUserName);
        void Update(Models.OmgevingsBoek_Models.Uitnodiging entityToUpdate);
    }
}
