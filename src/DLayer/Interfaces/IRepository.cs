using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DLayer
{
    public interface IRepository<T>
    {
        void Insert(T entity);
        void Delete(int id);
        void Edit(T entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
