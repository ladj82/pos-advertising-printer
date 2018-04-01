using System;
using System.Collections.Generic;
using System.Data;
using DapperExtensions;
using DapperExtensions.Mapper;
using PrintPDV.Persistence;
using PrintPDV.Utility.Models;

namespace PrintPDV.Controllers.Base
{
    public abstract class BaseController<T> : IBaseController<T> where T : class, IGenericEntity
    {
        private static IDatabase Database;

        public abstract void Validate(T entity);

        protected BaseController()
        {
            Database = DatabaseHandler.Instance();
        }

        public IDbConnection Connection {
            get { return Database.Connection; }
        }

        public bool HasActiveTransaction {
            get { return Database.HasActiveTransaction; }
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            Database.BeginTransaction(isolationLevel);
        }

        public void ClearCache()
        {
            Database.ClearCache();
        }

        public void Commit()
        {
            Database.Commit();
        }

        public int Count(object predicate, int? commandTimeout = null)
        {
            return Database.Count<T>(predicate, commandTimeout);
        }

        public int Count(object predicate, IDbTransaction transaction, int? commandTimeout = null)
        {
            return Database.Count<T>(predicate, transaction, commandTimeout);
        }

        public bool Delete(object predicate, int? commandTimeout = null)
        {
            return Database.Delete<T>(predicate, commandTimeout);
        }

        public bool Delete(T entity, int? commandTimeout = null)
        {
            return Database.Delete<T>(entity, commandTimeout);
        }

        public bool Delete(object predicate, IDbTransaction transaction, int? commandTimeout = null)
        {
            return Database.Delete<T>(predicate, transaction, commandTimeout);
        }

        public bool Delete(T entity, IDbTransaction transaction, int? commandTimeout = null)
        {
            return Database.Delete<T>(entity, transaction, commandTimeout);
        }

        public T Get(dynamic id, int? commandTimeout = null)
        {
            return Database.Get<T>(id, commandTimeout);
        }

        public T Get(dynamic id, IDbTransaction transaction, int? commandTimeout = null)
        {
            return Database.Get<T>(id, transaction, commandTimeout);
        }

        public IEnumerable<T> GetList(object predicate = null, IList<ISort> sort = null, int? commandTimeout = null, bool buffered = true)
        {
            return Database.GetList<T>(predicate, sort, commandTimeout, buffered);
        }

        public IEnumerable<T> GetList(object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout = null, bool buffered = true)
        {
            return Database.GetList<T>(predicate, sort, transaction, commandTimeout, buffered);
        }

        public IClassMapper GetMap()
        {
            return Database.GetMap<T>();
        }

        public IMultipleResultReader GetMultiple(GetMultiplePredicate predicate, int? commandTimeout = null)
        {
            return Database.GetMultiple(predicate, commandTimeout);
        }

        public IMultipleResultReader GetMultiple(GetMultiplePredicate predicate, IDbTransaction transaction, int? commandTimeout = null)
        {
            return Database.GetMultiple(predicate, transaction, commandTimeout);
        }

        public Guid GetNextGuid()
        {
            return Database.GetNextGuid();
        }

        public IEnumerable<T> GetPage(object predicate, IList<ISort> sort, int page, int resultsPerPage, int? commandTimeout = null, bool buffered = true)
        {
            return Database.GetPage<T>(predicate, sort, page, resultsPerPage, commandTimeout, buffered);
        }

        public IEnumerable<T> GetPage(object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout = null, bool buffered = true)
        {
            return Database.GetPage<T>(predicate, sort, page, resultsPerPage, transaction, commandTimeout, buffered);
        }

        public IEnumerable<T> GetSet(object predicate, IList<ISort> sort, int firstResult, int maxResults, int? commandTimeout, bool buffered)
        {
            return Database.GetSet<T>(predicate, sort, firstResult, maxResults, commandTimeout, buffered);
        }

        public IEnumerable<T> GetSet(object predicate, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout, bool buffered)
        {
            return Database.GetSet<T>(predicate, sort, firstResult, maxResults, transaction, commandTimeout, buffered);
        }

        public void Insert(IEnumerable<T> entities, int? commandTimeout = null)
        {
            Database.Insert<T>(entities, commandTimeout);
        }

        public dynamic Insert(T entity, int? commandTimeout = null)
        {
            Validate(entity);

            return Database.Insert<T>(entity, commandTimeout);
        }

        public void Insert(IEnumerable<T> entities, IDbTransaction transaction, int? commandTimeout = null)
        {
            Database.Insert<T>(entities, transaction, commandTimeout);
        }

        public dynamic Insert(T entity, IDbTransaction transaction, int? commandTimeout = null)
        {
            Validate(entity);

            return Database.Insert<T>(entity, transaction, commandTimeout);
        }

        public void Rollback()
        {
            Database.Rollback();
        }

        public void RunInTransaction(Action action)
        {
            Database.RunInTransaction(action);
        }

        public T RunInTransaction(Func<T> func)
        {
            return Database.RunInTransaction<T>(func);
        }

        public virtual dynamic Save(T entity, int? commandTimeout = null)
        {
            var dbEntity = Get(entity.Id);

            return dbEntity == null ? Insert(entity, commandTimeout) : Update(entity, commandTimeout);
        }

        public dynamic Save(T entity, IDbTransaction transaction, int? commandTimeout = null)
        {
            var dbEntity = Get(entity.Id);

            return dbEntity == null ? Insert(entity, transaction, commandTimeout) : Update(entity, transaction, commandTimeout);
        }

        public bool Update(T entity, int? commandTimeout = null)
        {
            Validate(entity);

            return Database.Update<T>(entity, commandTimeout);
        }

        public bool Update(T entity, IDbTransaction transaction, int? commandTimeout = null)
        {
            Validate(entity);

            return Database.Update<T>(entity, transaction, commandTimeout);
        }
    }
}
