using Core.Interfaces;
using DLayer.EFContext.EfEntities;
using DLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DLayer.EFContext.EfRepositories
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
            RemoveObject(_dbContext, _dbContext.Employee.Find(id));
        }

        public void Edit(Employee entity)
        {
            UpdateObject(_dbContext, entity);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbContext.Employee;
        }

        public IEnumerable<Employee> GetAllWithPaging(int pageNumber)
        {
            if (pageNumber < 1)
            {
                throw new Exception("Wrong pageNumber");
            }
            else
            {
                int pageSize = AppSetting.GetPageSize();
                return _dbContext.Employee.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
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
