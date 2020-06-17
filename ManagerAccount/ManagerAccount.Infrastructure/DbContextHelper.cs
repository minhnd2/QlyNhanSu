using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
namespace AppSolution.Infrastructure.Module.DataContext
{
    public class DbContextHelper<TDbContext> : IDisposable where TDbContext : DbContext, new()
    {
        private TDbContext db;
        public DbContextHelper()
        {
            db = new TDbContext();
        }

        public TDbContext DbContext
        {
            get
            {
                return db;
            }
        }
        #region Helper method
        /// <summary>
        /// Return a collection of objects of a particular type, where the type is defined by the T parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> GetTable<T>() where T : class
        {
            return db.Set<T>().AsQueryable<T>();
        }

        /// <summary>
        /// Get all data and return a list of objects of a particular type, where the type is defined by the T parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetAll<T>() where T : class
        {
            return db.Set<T>().ToList();
        }

        /// <summary>
        /// Get limited data by paging.
        /// Return a list of objects of a particular type, where the type is defined by the T parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T> GetAll<T>(int pageNumber, int pageSize) where T : class
        {
            if (pageNumber < 1)
                throw new Exception("Page number is invalid.");
            return db.Set<T>().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// Return a list of entities by filter (T parameter is a type of the entity).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<T> Filter<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return db.Set<T>().Where(predicate).ToList();
        }

        /// <summary>
        /// Return a list of entities by filter and paging (T parameter is a type of the entity).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<T> Filter<T>(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize)
        where T : class
        {
            if (pageNumber < 1)
                throw new Exception("Page number is invalid.");
            return db.Set<T>().Where(predicate).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// Return an entity if exist, or not return Null. (T parameter is a type of the entity)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T GetOne<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return db.Set<T>().Where(predicate).SingleOrDefault();
        }
        /// <summary>
        /// Check an entity is exist in database. (T parameter is a type of the entity).
        /// Return True if the entity is exist.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool EntityExists<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return db.Set<T>().Where(predicate).Count() > 0;
        }
        /// <summary>
        /// Insert an entity into database (T parameter is a type of the entity).
        /// Notes: After inserting, this entity is mapping with database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void Insert<T>(T entity) where T : class
        {
            db.Set<T>().Add(entity);
            db.SaveChanges();
        }
        /// <summary>
        /// Insert a list of entities (T parameter is a type of the entity).
        /// Notes: After inserting, this list is mapping with database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityList"></param>
        public void Insert<T>(List<T> entityList) where T : class
        {
            db.Set<T>().AddRange(entityList);
            db.SaveChanges();
        }

        /// <summary>
        /// Update an entity by expression (T parameter is a type of the entity).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        public void Update<T>(T entity, Expression<Func<T, bool>> predicate) where T : class
        {
            var t = db.Set<T>().Where(predicate).SingleOrDefault();
            if (t == null)
                throw new NullReferenceException("Error: Entity is not exist.");
            if (!t.Equals(entity))
                foreach (var p in t.GetType().GetProperties())
                    p.SetValue(t, p.GetValue(entity, null), null);
            db.SaveChanges();
        }

        public void Update<T>(T entity) where T : class
        {
            db.Entry<T>(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void AddOrUpdate<T>(T entity, Expression<Func<T, bool>> predicate) where T : class
        {
            var t = db.Set<T>().Where(predicate).SingleOrDefault();
            if (t == null)
            {
                this.Insert<T>(entity);
            }
            else
            {
                if (!t.Equals(entity))
                {
                    foreach (var p in t.GetType().GetProperties())
                        p.SetValue(t, p.GetValue(entity, null), null);
                    db.Entry<T>(t).State = EntityState.Modified;
                }
            }
            db.SaveChanges();
        }

        /// <summary>
        /// Delete data of one table has name is T parameter by expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        public void Delete<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var t = db.Set<T>().Where(predicate);
            if (t.Count() == 0) return;
            db.Set<T>().RemoveRange(t);
            db.SaveChanges();
        }

        /// <summary>
        /// Delete all data of one table has name is T parameter.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void DeleteAll<T>() where T : class
        {
            var t = db.Set<T>();
            if (t.Count() == 0) return;
            db.Set<T>().RemoveRange(t);
            db.SaveChanges();
        }
        #endregion

        #region Transaction Controller
        private DbContextTransaction Transaction { get; set; }
        public void BeginTransaction()
        {
            if (this.Transaction == null)
                this.Transaction = db.Database.BeginTransaction();
        }
        public void RollbackTransaction()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Rollback();
                Dispose();
            }

        }

        public void CommitTransaction()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Commit();
                Dispose();
            }

        }
        #endregion

        public void SaveChange()
        {
            db.SaveChanges();
        }
        public async Task<int> SaveChangeAsync()
        {
            return await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (this.Transaction != null)
            {
                this.Transaction.Dispose();
                this.Transaction = null;
            }
            db.Dispose();
            db = null;            
        }

    }
}
