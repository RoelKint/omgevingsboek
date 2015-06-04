using System;
namespace BusinessLogic.Repositories
{
    public interface IPoiRepository
    {
        System.Collections.Generic.IEnumerable<Models.OmgevingsBoek_Models.Poi> All();
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> get50();
        System.Collections.Generic.List<Models.OmgevingsBoek_Models.Poi> get50From(int from);
        Models.OmgevingsBoek_Models.Poi Insert(Models.OmgevingsBoek_Models.Poi entity);
    }
}
