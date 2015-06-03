using System;
namespace BusinessLogic.Services
{
    public interface IBoekService
    {
        void DeleteBoek(Models.OmgevingsBoek_Models.Boek boek);
        void EditBoek(Models.OmgevingsBoek_Models.Boek boek);
        Models.OmgevingsBoek_Models.Activiteit GetActiviteitById(int id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActiviteitenList();
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetActivitiesByUsername(string Username);
        Models.OmgevingsBoek_Models.Boek GetBoekByID(object id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> GetBoekenByUser(string username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> GetPoiList();
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetSharedActivitiesByBookId(int BoekId, string Username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> GetSharedActivitiesByUsername(string Username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> GetSharedBoeken(string username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Tag> GetTagList();
        Models.MVC_Models.ApplicationUser GetUser(string Username);
        Models.OmgevingsBoek_Models.Boek InsertBoek(Models.OmgevingsBoek_Models.Boek boek);
        Models.OmgevingsBoek_Models.Poi InsertPoi(Models.OmgevingsBoek_Models.Poi poi);
        Models.OmgevingsBoek_Models.Tag InsertTag(Models.OmgevingsBoek_Models.Tag tag);
    }
}
