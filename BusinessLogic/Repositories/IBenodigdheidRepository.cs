using System;
namespace BusinessLogic.Repositories
{
    public interface IBenodigdheidRepository
    {
        System.Collections.Generic.IEnumerable<Models.OmgevingsBoek_Models.Benodigdheid> All();
        Models.OmgevingsBoek_Models.Benodigdheid Insert(string entity);
    }
}
