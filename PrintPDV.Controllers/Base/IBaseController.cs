using System;
using System.Collections.Generic;
using System.Data;
using DapperExtensions;
using DapperExtensions.Mapper;
using PrintPDV.Utility.Models;

namespace PrintPDV.Controllers.Base
{
    public interface IBaseController<T> where T : class, IGenericEntity
    {
        IDbConnection Connection { get; }
        
        bool HasActiveTransaction { get; }
        
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        
        void ClearCache();
        
        void Commit();
        
        int Count(object predicate, int? commandTimeout = null);
        
        int Count(object predicate, IDbTransaction transaction, int? commandTimeout = null);
        
        bool Delete(object predicate, int? commandTimeout = null);
        
        bool Delete(T entity, int? commandTimeout = null);
        
        bool Delete(object predicate, IDbTransaction transaction, int? commandTimeout = null);
        
        bool Delete(T entity, IDbTransaction transaction, int? commandTimeout = null);
        
        T Get(dynamic id, int? commandTimeout = null);
        
        T Get(dynamic id, IDbTransaction transaction, int? commandTimeout = null);
        
        IEnumerable<T> GetList(object predicate = null, IList<ISort> sort = null, int? commandTimeout = null, bool buffered = true);
        
        IEnumerable<T> GetList(object predicate, IList<ISort> sort, IDbTransaction transaction, int? commandTimeout = null, bool buffered = true);
        
        IClassMapper GetMap();
        
        IMultipleResultReader GetMultiple(GetMultiplePredicate predicate, int? commandTimeout = null);
        
        IMultipleResultReader GetMultiple(GetMultiplePredicate predicate, IDbTransaction transaction, int? commandTimeout = null);
        
        Guid GetNextGuid();
        
        IEnumerable<T> GetPage(object predicate, IList<ISort> sort, int page, int resultsPerPage, int? commandTimeout = null, bool buffered = true);
        
        IEnumerable<T> GetPage(object predicate, IList<ISort> sort, int page, int resultsPerPage, IDbTransaction transaction, int? commandTimeout = null, bool buffered = true);
        
        IEnumerable<T> GetSet(object predicate, IList<ISort> sort, int firstResult, int maxResults, int? commandTimeout, bool buffered);
        
        IEnumerable<T> GetSet(object predicate, IList<ISort> sort, int firstResult, int maxResults, IDbTransaction transaction, int? commandTimeout, bool buffered);
        
        void Insert(IEnumerable<T> entities, int? commandTimeout = null);
        
        dynamic Insert(T entity, int? commandTimeout = null);
        
        void Insert(IEnumerable<T> entities, IDbTransaction transaction, int? commandTimeout = null);
        
        dynamic Insert(T entity, IDbTransaction transaction, int? commandTimeout = null);
        
        void Rollback();
        
        void RunInTransaction(Action action);

        dynamic Save(T entity, int? commandTimeout = null);

        dynamic Save(T entity, IDbTransaction transaction, int? commandTimeout = null);
        
        T RunInTransaction(Func<T> func);
        
        bool Update(T entity, int? commandTimeout = null);
        
        bool Update(T entity, IDbTransaction transaction, int? commandTimeout = null);

        void Validate(T entity);
    }
}