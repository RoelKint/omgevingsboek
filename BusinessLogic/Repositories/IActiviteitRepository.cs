using System;
namespace BusinessLogic.Repositories
{
    public interface IActiviteitRepository
    {
        System.Collections.Generic.IEnumerable<Models.OmgevingsBoek_Models.Activiteit> All();
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getActivitiesByUsername(string Username);
        Models.OmgevingsBoek_Models.Activiteit GetByID(object id);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getSharedActivitiesByBookId(int BoekId, string Username);
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Activiteit> getSharedActivitiesByUsername(string Username);
    }
}
