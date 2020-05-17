using DataModal.DataClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DataModal.GenericRepository
{
    public class SkyCoGenericRepository<TEntity> where TEntity : class
    {
        #region Member
        internal SkyCoDbContext dbcontext;
        internal DbSet<TEntity> dbSet;
        #endregion

        #region Contructor
        public SkyCoGenericRepository(SkyCoDbContext context)
        {
            this.dbcontext = context;
            this.dbSet = context.Set<TEntity>();
        }
        #endregion

        #region CUD
        public virtual void Create(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public virtual void Update(TEntity entity, List<string> modifiedfields)
        {
            dbcontext.Entry<TEntity>(entity).State = EntityState.Unchanged;
            foreach (string var in modifiedfields)
            {
                dbcontext.Entry<TEntity>(entity).Property(var).IsModified = true;
            }
        }
        public virtual void Delete(TEntity entity, List<string> modifiedfields)
        {
            this.Update(entity, modifiedfields);
        }


        #endregion

        #region ReadOne
        public virtual TEntity GetById(Int64 ID)
        {
            return dbSet.Find(ID);
        }

        public virtual TEntity GetOneByFilters(Expression<Func<TEntity, bool>> where, params string[] include)
        {
            IQueryable<TEntity> query = this.dbSet;
            if (include != null)
                query = include.Aggregate(query, (current, inc) => current.Include(inc));
            if (where != null)
                query = query.Where(where);
            return query.FirstOrDefault<TEntity>();
        }

        #endregion

        #region ReadAll
        public virtual IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = this.dbSet;
            return query;
        }

        public virtual IQueryable<TEntity> GetAllByFilters(Expression<Func<TEntity, bool>> where, params string[] include)
        {
            IQueryable<TEntity> query = this.dbSet;
            if (include != null)
                query = include.Aggregate(query, (current, inc) => current.Include(inc));
            if (where != null)
                query = query.Where(where);
            return query;
        }
        #endregion
    }
}
