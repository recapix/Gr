using GR.Data.Entities;
using System;
using System.Collections.Generic;

namespace GR.Data.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Query(Func<T, bool> predicate);
        T Get(Guid id);
        T Get(object[] id);
        void Insert(T entity);        
        void Update(T entity);
        void Delete(T entity);
        void Delete(Func<T, bool> predicate);
    }
}
