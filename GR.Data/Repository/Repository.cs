using GR.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GR.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(ApplicationContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T Get(Guid id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }

        public T Get(object[] id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }
            return entities.Find(id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public void Delete(Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Where(predicate).ToList()
                .ForEach(del => context.Set<T>().Remove(del));
        }

        public IEnumerable<T> Query(Func<T, bool> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("entity");
            }
            return entities.Where(predicate);
        }        
    }
}
