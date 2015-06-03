using System;
namespace BusinessLogic.Services
{
    public interface IBoekService
    {
        void DeleteBoek(Models.OmgevingsBoek_Models.Boek boek);
        void EditBoek(Models.OmgevingsBoek_Models.Boek boek);
        Models.OmgevingsBoek_Models.Activiteit getActiviteitById(int id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getActiviteitenList();
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getActivitiesByUsername(string Username);
        Models.OmgevingsBoek_Models.Boek GetBoekByID(object id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> getBoekenByUser(string username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getSharedActivitiesByBookId(int BoekId, string Username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getSharedActivitiesByUsername(string Username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Boek> getSharedBoeken(string username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Tag> getTagList();
        Models.MVC_Models.ApplicationUser getUser(string Username);
        Models.OmgevingsBoek_Models.Boek InsertBoek(Models.OmgevingsBoek_Models.Boek boek);
        Models.OmgevingsBoek_Models.Tag InsertTag(Models.OmgevingsBoek_Models.Tag tag);
    }
}
