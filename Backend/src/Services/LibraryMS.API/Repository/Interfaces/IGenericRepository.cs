using System.Linq.Expressions;

namespace LibraryMS.API.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Delete(TEntity entityToDelete);
        void Delete(object id);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        Task<TEntity> GetByIDAsync(object id);
        TEntity GetByID(object id);

        void Insert(TEntity entity);
        void InsertRange(List<TEntity> entity);
        void Update(TEntity entityToUpdate);
        void UpdateRange(List<TEntity> entityToUpdate);
        Task<Int64> GetMaxIDAsync(Expression<Func<TEntity, Int64?>> filter);
        Int64? GetMaxID(Expression<Func<TEntity, Int64?>> filter);
        Int64? GetMaxID_1(Expression<Func<TEntity, Int64?>> filter);
        void DeleteWhere(Expression<Func<TEntity, bool>> filter = null);


        void DeleteRange(List<TEntity> entityToDelete);
    }
}
