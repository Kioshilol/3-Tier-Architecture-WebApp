using DLayer.EF.EfEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DLayer.EF
{
    public abstract class BaseRepository<T>
    {
        protected void Remove(TrainingTaskContext dbContext,T entity)
        {
            T nameOfClass = entity;
            if (nameOfClass != null)
                dbContext.Remove(nameOfClass);
            dbContext.SaveChanges();
        }

        protected void Update(TrainingTaskContext dbContext, T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
