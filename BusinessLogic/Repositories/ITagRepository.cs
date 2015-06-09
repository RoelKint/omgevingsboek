using System;
namespace BusinessLogic.Repositories
{
    public interface ITagRepository
    {
        System.Collections.Generic.IEnumerable<Models.OmgevingsBoek_Models.Tag> All();
        Models.OmgevingsBoek_Models.Tag Insert(string entity);
    }
}
