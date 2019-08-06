using Core;
using Core.Interfaces;
using DLayer.EFContext.EfEntities;
using DLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DLayer.EFContext.EfRepositories
{
    public class EmployeeEfRepository :BaseRepository<Employee>, IEmployeeRepository<Employee>
    {
        private TrainingTaskContext _dbContext;
        public EmployeeEfRepository(TrainingTaskContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Delete(int id)
        {
            Employee employee = _dbContext.Employee.Include(e => e.EmployeeTasks).FirstOrDefault(e => e.Id == id);
            _dbContext.Remove(employee);
            _dbContext.SaveChanges();
        }

        public void Edit(Employee entity)
        {
            UpdateObject(_dbContext, entity);
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = _dbContext.Employee;
            return employees;
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

        public IEnumerable<Employee> GetEmployeesByTaskId(int id)
        {
            return _dbContext.Employee.Where(e => e.EmployeeTasks.Any(et => e.Id == et.EmployeeId && et.TaskId == id)).ToList();
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
