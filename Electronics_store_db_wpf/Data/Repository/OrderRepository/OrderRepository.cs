using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.EFcommonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electronics_store_db_wpf.Data.Repository.OrderRepository
{
    public class OrderRepository : EFcommonRepository<Order>, IOrderRepository
    {
        private readonly IServiceProvider serviceProvider;
        public OrderRepository(IServiceProvider _serviceProvider) : base(_serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }
        
        public override async Task<Order> GetByIdAsync(Guid id)
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                  .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    var entity = await dbContext.Set<Order>().Include(o=>o.OrderItems).FirstOrDefaultAsync(p => p.Id == id);

                    return entity;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public override async Task<bool> AddAsync(Order order)
        {
            if (order?.Client == null)
            {
                return false;
            }
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                    .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    var client = dbContext.Set<Client>().FirstOrDefault(c=>c.Id== order.Client.Id);
                    
                    if (client == null) { return false; }

                    client.Orders.Add(order);

                    dbContext.Update(client);

                    await dbContext.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception e)
            {

                return false;
            }

        }
        public override async Task<bool> UpdateAsync(Order order)
        {
            if (order?.Client == null)
            {
                return false;
            }
            if (order == null)
            {
                return false;
            }
       
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                   .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    var existingOrder = await dbContext.Set<Order>().FirstOrDefaultAsync(x => x.Id == order.Id);

                    if (existingOrder == null)
                    {
                        return false;
                    }

                    existingOrder.UpdateProperties(order);

                    dbContext.Update(existingOrder);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<List<Order>> GetAllIncludeOrderItemsAsync()
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                   .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    List<Order> entityList = await dbContext.Set<Order>().Include(o=>o.OrderItems).ToListAsync();

                    return entityList;
                }
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public override async Task<bool> DeleteByIdAsync(Guid id)
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                  .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    var entity = await dbContext.Set<Order>().Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);

                    if (entity == null)
                    {
                        return false;
                    }

                    dbContext.Set<Order>().Remove(entity);
                    await dbContext.SaveChangesAsync();
                    return true;

                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public override async Task<bool> DeleteByIdsAsync(List<Guid> ids)
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                  .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    var entities = await dbContext.Set<Order>().Include(o => o.OrderItems).Where(e => ids.Contains(e.Id)).ToListAsync();
                    if (entities == null)
                    {
                        return false;
                    }

                    foreach (var item in entities)
                    {
                        dbContext.Set<Order>().Remove(item);
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

        public async Task<List<Order>> GetByClientIdAsync(Guid clientId)
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                  .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    var entity = await dbContext.Set<Client>()
                        .Include(o => o.Orders)
                        .FirstOrDefaultAsync(p => p.Id == clientId);

                    var entityList = entity?.Orders.ToList();

              
                   
                    return entityList;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
