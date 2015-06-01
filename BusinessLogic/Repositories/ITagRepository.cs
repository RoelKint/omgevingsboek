using System;
namespace BusinessLogic.Repositories
{
    public interface ITagRepository
    {
        Models.OmgevingsBoek_Models.Tag Insert(Models.OmgevingsBoek_Models.Tag entity);
    }
}
