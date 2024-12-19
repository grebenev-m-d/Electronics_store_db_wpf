using Electronics_store_db_wpf.Data.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Electronics_store_db_wpf.Data.Repository.EFcommonRepository
{
    public class EFcommonRepository<T> : IEFcommonRepository<T> where T : BaseEntity
    {

        private readonly IServiceProvider serviceProvider;
        public EFcommonRepository(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;

        }

        public virtual async Task<bool> CheckRecordByIdAsync(Guid id)
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                    .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    bool exists = await dbContext.Set<T>().AnyAsync(x => x.Id == id);

                    return exists;
                }
            }
            catch (Exception e)
            {

                return false;
            }
        }
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                  .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    T entity = await dbContext.Set<T>().FirstOrDefaultAsync(p => p.Id == id);

                    return entity;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                    .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                   await dbContext.Set<T>().AddAsync(entity);
                   await dbContext.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception e)
            {

                return false;
            }

        }
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                   .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    bool exists = await dbContext.Set<T>().AnyAsync(x => x.Id == entity.Id);

                    if(!exists) 
                    {
                        return false;
                    }

                    dbContext.Set<T>().Update(entity);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public virtual async Task<List<T>> GetAllAsync()
        {

            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                   .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    List<T> entityList = await dbContext
                        .Set<T>()
                        .ToListAsync();

                    return entityList;
                }
            }
            catch (Exception e)
            {

                return null;
            }
        }



        public virtual async Task<bool> DeleteByIdAsync(Guid id)
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                  .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    var entity = await dbContext
                        .Set<T>().FindAsync(id);

                    if (entity == null)
                    {
                        return false;
                    }

                    dbContext.Set<T>().Remove(entity);
                    await dbContext.SaveChangesAsync();
                    return true;

                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public virtual async Task<bool> DeleteByIdsAsync(List<Guid> ids)
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                  .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    var entities = await dbContext.Set<T>()
                        .Where(e => ids
                        .Contains(e.Id))
                        .ToListAsync();
                    if (entities == null)
                    {
                        return false;
                    }

                    foreach (var item in entities)
                    {
                        dbContext.Set<T>().Remove(item);
                    }
                    await dbContext.SaveChangesAsync();
                    return true;

                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
