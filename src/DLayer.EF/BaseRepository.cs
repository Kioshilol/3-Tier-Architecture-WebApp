using DLayer.EFContext.EfEntities;
using Microsoft.EntityFrameworkCore;

namespace DLayer.EFContext
{
    public abstract class BaseRepository<T>
    {
        protected void UpdateObject(TrainingTaskContext dbContext, T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
