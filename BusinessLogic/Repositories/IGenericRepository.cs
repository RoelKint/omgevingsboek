using System;
namespace BusinessLogic.Repositories
{
    interface IGenericRepository<TEntity>
     where TEntity : class
    {
        System.Collections.Generic.IEnumerable<TEntity> All();
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        TEntity GetByID(object id);
        TEntity Insert(TEntity entity);
        void SaveChanges();
        void Update(TEntity entityToUpdate);
    }
}
