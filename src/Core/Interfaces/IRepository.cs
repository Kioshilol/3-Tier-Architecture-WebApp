using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IRepository<T>
    {
        int Insert(T entity);
        void Delete(int id);
        void Edit(T entity);
        IEnumerable<T> GetAllWithPaging(int PageNumber);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
