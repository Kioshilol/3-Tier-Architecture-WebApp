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
    public class TaskEfRepository :BaseRepository<Task>, ITaskRepository<Task>
    {
        private TrainingTaskContext _dbContext;
        public TaskEfRepository(TrainingTaskContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public void Delete(int id)
        {
            Task task = _dbContext.Task.Include(t => t.EmployeeTasks).FirstOrDefault(t => t.Id == id);
            _dbContext.Remove(task);
            _dbContext.SaveChanges();
        }

        public void Edit(Task entity)
        {
            foreach(var item in entity.EmployeeTasks)
            {
                var ee = _dbContext.EmployeeTasks.FirstOrDefault(e => e.TaskId == item.TaskId);
                _dbContext.EmployeeTasks.Remove(ee);
                _dbContext.EmployeeTasks.Add(new EmployeeTasks { EmployeeId = item.EmployeeId, TaskId =entity.Id });
            }

            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public IEnumerable<Task> GetAll()
        {
            return _dbContext.Task;
        }

        public IEnumerable<Task> GetAllTasksByProjectId(int id)
        {
            return _dbContext.Task.Where(task => task.ProjectId == id);
        }

        public IEnumerable<Task> GetAllWithPaging(int pageNumber)
        {
            if (pageNumber < 1)
            {
                throw new Exception("Wrong pageNumber");
            }
            else
            {
                var tasks = _dbContext.Task.Include(t => t.EmployeeTasks).ThenInclude(e => e.Employee).ToList();
                int pageSize = AppSetting.GetPageSize();
                return tasks.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public Task GetById(int id)
        {
            var tasks = _dbContext.Task.Include(t => t.EmployeeTasks).ThenInclude(e => e.Employee).ToList();
            foreach (var task in tasks)
            {
                if (task.Id == id)
                    return _dbContext.Task.Find(id);
            }
            return null;
        }

        public int Insert(Task entity)
        {
            _dbContext.Task.Add(entity);
            _dbContext.SaveChanges();
            _dbContext.Entry(entity).GetDatabaseValues();
            return entity.Id;
        }
    }
}
