using DLayer.EFContext.EfEntities;
using Microsoft.EntityFrameworkCore;

namespace DLayer.EFContext
{
    public abstract class BaseRepository<T>
    {
        protected void RemoveObject(TrainingTaskContext dbContext,T entity)
        {
            T nameOfClass = entity;
            if (nameOfClass != null)
                dbContext.Remove(nameOfClass);
            dbContext.SaveChanges();
        }

        protected void UpdateObject(TrainingTaskContext dbContext, T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
