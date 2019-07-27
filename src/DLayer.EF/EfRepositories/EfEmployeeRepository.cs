using Core.Interfaces;
using DLayer.EF.EfEntities;
using DLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DLayer.EF.EfRepositories
{
    public class EfEmployeeRepository :BaseRepository<Employee>, IRepository<Employee>
    {
        private TrainingTaskContext _dbContext;
        public EfEmployeeRepository(TrainingTaskContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            Remove(_dbContext, _dbContext.Employee.Find(id));
        }

        public void Edit(Employee entity)
        {
            Update(_dbContext, entity);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbContext.Employee;
        }

        public IEnumerable<Employee> GetAllWithPaging(int PageNumber)
        {
            return _dbContext.Employee;
        }

        public Employee GetById(int id)
        {
            return _dbContext.Employee.Find(id);
        }

        public int Insert(Employee entity)
        {
            _dbContext.Employee.Add(entity);
            _dbContext.SaveChanges();
            _dbContext.Entry(entity).GetDatabaseValues();
            return entity.Id.Value;
        }
    }
}
