using BLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLayer.Interfaces
{
    public interface IService<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        int Add(T entity);
        void Edit(T entity);
        void Delete(int id);
    }
}
