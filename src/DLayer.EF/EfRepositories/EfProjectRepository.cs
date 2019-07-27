using Core.Interfaces;
using DLayer.EF.EfEntities;
using DLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLayer.EF.EfRepositories
{
    public class EfProjectRepository :BaseRepository<Project>, IRepository<Project>
    {
        private TrainingTaskContext _dbContext;
        public EfProjectRepository(TrainingTaskContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Delete(int id)
        {
            Remove(_dbContext, _dbContext.Project.Find(id));
        }

        public void Edit(Project entity)
        {
            Update(_dbContext, entity);
        }

        public IEnumerable<Project> GetAll()
        {
            return _dbContext.Project;
        }

        public IEnumerable<Project> GetAllWithPaging(int PageNumber)
        {

            return _dbContext.Project;
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
