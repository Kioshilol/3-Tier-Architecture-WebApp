using Core;
using Core.Interfaces;
using DLayer.EFContext.EfEntities;
using DLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLayer.EFContext.EfRepositories
{
    public class EfTaskRepository :BaseRepository<Task>, ITaskRepository<Task>
    {
        private TrainingTaskContext _dbContext;
        public EfTaskRepository(TrainingTaskContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public void Delete(int id)
        {
            RemoveObject(_dbContext, _dbContext.Task.Find(id));
        }

        public void Edit(Task entity)
        {
            UpdateObject(_dbContext, entity);
        }

        public IEnumerable<Task> GetAll()
        {
            return _dbContext.Task;
        }

        public IEnumerable<Task> GetAllTasksByProjectId(int id)
        {
            return _dbContext.Task;
        }

        public IEnumerable<Task> GetAllWithPaging(int pageNumber)
        {
            if (pageNumber < 1)
            {
                throw new Exception("Wrong pageNumber");
            }
            else
            {
                var list = _dbContext.Task;
                int pageSize = AppSetting.GetPageSize();
                return _dbContext.Task.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public Task GetById(int id)
        {
            return _dbContext.Task.Find(id);
        }

        public int Insert(Task entity)
        {
            _dbContext.Task.Add(entity);
            _dbContext.SaveChanges();
            _dbContext.Entry(entity).GetDatabaseValues();
            foreach (var id in entity.EmployeeId)
            {
                EmployeeTasks employeeTasks = new EmployeeTasks();
                employeeTasks.TaskId = entity.Id;
                employeeTasks.EmployeeId = id;
                _dbContext.EmployeeTasks.Add(employeeTasks);
                _dbContext.SaveChanges();
            }
            return entity.Id;
        }
    }
}
