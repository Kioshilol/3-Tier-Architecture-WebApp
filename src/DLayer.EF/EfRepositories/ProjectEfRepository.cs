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
    public class ProjectEFRepository :BaseRepository<Project>, IRepository<Project>
    {
        private TrainingTaskContext _dbContext;
        public ProjectEFRepository(TrainingTaskContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Delete(int id)
        {
            Project project = _dbContext.Project.Include(p => p.Tasks).FirstOrDefault(p => p.Id == id);

            foreach (var item in project.Tasks)
            {
                Task task = _dbContext.Task.Include(t => t.EmployeeTasks).FirstOrDefault(t => t.Id == item.Id);
                _dbContext.Task.Remove(task);
            }

            _dbContext.Project.Remove(project);
            _dbContext.SaveChanges();
        }

        public void Edit(Project entity)
        {
            UpdateObject(_dbContext, entity);
        }

        public IEnumerable<Project> GetAll()
        {
            return _dbContext.Project;
        }

        public IEnumerable<Project> GetAllWithPaging(int pageNumber)
        {
            if (pageNumber < 1)
            {
                throw new Exception("Wrong pageNumber");
            }
            else
            {
                int pageSize = AppSetting.GetPageSize();
                return _dbContext.Project.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public Project GetById(int id)
        {
            return _dbContext.Project.Find(id);
        }

        public int Insert(Project entity)
        {
            _dbContext.Project.Add(entity);
            _dbContext.SaveChanges();
            _dbContext.Entry(entity).GetDatabaseValues();
            return entity.Id.Value;
        }
    }
}
