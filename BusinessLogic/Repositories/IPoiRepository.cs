using System;
namespace BusinessLogic.Repositories
{
    public interface IPoiRepository
    {
        System.Collections.Generic.IEnumerable<Models.OmgevingsBoek_Models.Poi> All();
        Models.OmgevingsBoek_Models.Poi Insert(Models.OmgevingsBoek_Models.Poi entity);
    }
}
