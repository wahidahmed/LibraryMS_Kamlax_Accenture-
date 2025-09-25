using LibraryMS.API.Data;
using LibraryMS.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryMS.API.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private AppDbContext context;
        private DbSet<TEntity> dbSet;

        public GenericRepository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.AsNoTracking().Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.AsNoTracking().Include(includeProperty);
                }
            }


            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.AsNoTracking().Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.AsNoTracking().Include(includeProperty);
                }
            }


            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }


        public virtual async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.AsNoTracking().Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.AsNoTracking().Include(includeProperty);
                }
            }


            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            else
            {
                return await query.FirstOrDefaultAsync();
            }
        }

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }


            if (orderBy != null)
            {
                return orderBy(query).FirstOrDefault();
            }
            else
            {
                return query.FirstOrDefault();
            }
        }

        public virtual async Task<TEntity> GetByIDAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }



        public virtual async Task<Int64> GetMaxIDAsync(Expression<Func<TEntity, Int64?>> filter)
        {
            //int? id = await dbSet.MaxAsync(filter) != null ? await dbSet.MaxAsync(filter) : 0;

            Int64 id = await dbSet.MaxAsync(filter) != null ? Convert.ToInt64(await dbSet.MaxAsync(filter)) : 0;
            return id;
        }

        public virtual Int64? GetMaxID(Expression<Func<TEntity, Int64?>> filter)
        {
            Int64? id = dbSet.Max(filter) != null ? dbSet.Max(filter) : 0;
            return id;
        }

        public virtual Int64? GetMaxID_1(Expression<Func<TEntity, Int64?>> filter)
        {
            Int64? id = dbSet.Max(filter) != null ? dbSet.Max(filter) : 0;
            return id;
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void InsertRange(List<TEntity> entity)
        {
            dbSet.AddRange(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void DeleteWhere(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
            {
                dbSet.RemoveRange(dbSet.Where(filter));
            }
            //var dbSet = context.Set<TEntity>();
            //if (predicate != null)
            //    dbSet.RemoveRange(dbSet.Where(predicate));
            //else
            //    dbSet.RemoveRange(dbSet);

            //context.SaveChanges();
        }
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void DeleteRange(List<TEntity> entityToDelete)
        {

            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.AttachRange(entityToDelete);
            }
            dbSet.RemoveRange(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            context.Update(entityToUpdate);
            //dbSet.Attach(entityToUpdate);

            //context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void UpdateRange(List<TEntity> entityToUpdate)
        {

            //dbSet.AttachRange(entityToUpdate);
            context.UpdateRange(entityToUpdate);
            //context.Entry(entityToUpdate).State = EntityState.Modified;

        }

    }
}
