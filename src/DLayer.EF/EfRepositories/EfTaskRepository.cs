using Core.Interfaces;
using DLayer.EF.EfEntities;
using DLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLayer.EF.EfRepositories
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
            Remove(_dbContext, _dbContext.Task.Find(id));
        }

        public void Edit(Task entity)
        {
            Update(_dbContext, entity);
        }

        public IEnumerable<Task> GetAll()
        {
            return _dbContext.Task;
        }

        public IEnumerable<Task> GetAllTasksByProjectId(int id)
        {
            return _dbContext.Task.Where(p => p.ProjectId == id);
        }

        public IEnumerable<Task> GetAllWithPaging(int PageNumber)
        {
            return _dbContext.Task;
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
            return entity.Id;
        }
    }
}
