using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Order_servies.Models;

namespace Order_servies.Infastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Order_Context context;  
        private DbSet < T > entities;  
        string errorMessage = string.Empty;  
        public Repository(Order_Context context) {  
            this.context = context;  
            entities = context.Set < T > ();  
        }  
       #region Implementation
        public virtual T Add(T entity)
        {
             entities.Add(entity);
             context.SaveChanges();
             return entity;
        }

        public virtual void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.Set<T>().Attach(entity);
            context.SaveChanges();
        }

        public virtual T Delete(T entity)
        {
            entities.Remove(entity);
            context.SaveChanges();
            return entity;
        }
        public virtual T Delete(int id)
        {
            var entity = entities.Find(id);
            entities.Remove(entity);
            context.SaveChanges();
            return entity;
        }
        public virtual void DeleteMulti(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = context.Set<T>().Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                context.Set<T>().Remove(obj);
            context.SaveChanges();
        }

        public virtual T GetSingleById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string includes)
        {
            return context.Set<T>().Where(where).ToList();
        }


        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return context.Set<T>().Count(where);
        }

        public IEnumerable<T> GetAll(string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = context.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.AsQueryable();
            }

            return context.Set<T>().AsQueryable();
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = context.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.FirstOrDefault(expression);
            }
            return context.Set<T>().FirstOrDefault(expression);
        }

        public virtual IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = context.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where<T>(predicate).AsQueryable<T>();
            }

            return context.Set<T>().Where<T>(predicate).AsQueryable<T>();
        }

        public virtual IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> predicate, out int total, int index = 0, int size = 20, string[] includes = null)
        {
            int skipCount = index * size;
            IQueryable<T> _resetSet;

            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = context.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                _resetSet = predicate != null ? query.Where<T>(predicate).AsQueryable() : query.AsQueryable();
            }
            else
            {
                _resetSet = predicate != null ? context.Set<T>().Where<T>(predicate).AsQueryable() : context.Set<T>().AsQueryable();
            }

            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public bool CheckContains(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Count<T>(predicate) > 0;
        }
        #endregion
    }
}
