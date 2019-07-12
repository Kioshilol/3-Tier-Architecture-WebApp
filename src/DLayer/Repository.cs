using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DLayer
{
    class Repository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> DbSet;

        public Repository(EFDBContext context)
        {
            DbSet = context.Set<T>();
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {

            return DbSet;
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }
        //public void Update(T entity)
        //{
            //??
        //}
    }
}
