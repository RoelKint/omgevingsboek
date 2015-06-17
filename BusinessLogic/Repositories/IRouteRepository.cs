using System;
namespace BusinessLogic.Repositories
{
    public interface IRouteRepository
    {
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Route> getRoutesByBoek(int boekId);
        Models.OmgevingsBoek_Models.Route Insert(Models.OmgevingsBoek_Models.Route entity);
    }
}
