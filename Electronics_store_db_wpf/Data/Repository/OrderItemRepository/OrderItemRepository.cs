using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.EFcommonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electronics_store_db_wpf.Data.Repository.OrderItemRepository
{
    public class OrderItemRepository : EFcommonRepository<OrderItem>, IOrderItemRepository
    {
        private readonly IServiceProvider serviceProvider;
        public OrderItemRepository(IServiceProvider _serviceProvider) : base(_serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }

        public async Task<List<OrderItem>> GetItemsByOrderIdIncludingProductsAsync(Guid orderId)
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                  .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    var entity = await dbContext.Set<Order>()
                        .Include(o => o.OrderItems)
                        .ThenInclude(o => o.Product)
                        .FirstOrDefaultAsync(p => p.Id == orderId);

                    var entityList = entity?.OrderItems.ToList();

                    /*   Don't mind me    =)  */ entityList?.ForEach(item => item.Product ??= new Product());

                    return entityList;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }



        public override async Task<OrderItem> GetByIdAsync(Guid id)
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                  .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    var entity = await dbContext.Set<OrderItem>()
                        .Include(o => o.Order)
                        .Include(o => o.Product)
                        .FirstOrDefaultAsync(p => p.Id == id);

                    return entity;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public override async Task<bool> AddAsync(OrderItem orderItem)
        {
            if (orderItem?.Order == null)
            {
                return false;
            }
            if (orderItem?.Product == null)
            {
                return false;
            }
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                    .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {

                    var exists = dbContext.Set<OrderItem>().Any(p => p.Id == orderItem.Id);
                    if (exists)
                    {
                        return false;
                    }

                    var order = dbContext.Set<Order>().FirstOrDefault(o => o.Id == orderItem.Order.Id);
                    if (order == null)
                    {
                        return false;
                    }

                    var product = dbContext.Set<Product>().FirstOrDefault(p => p.Name == orderItem.Product.Name);
                    if (product == null)
                    {
                        return false;
                    }
                    //orderItem.ProductId = product.Id;
                    //orderItem.OrderId = order.Id;

                    orderItem.Product = product;
                    orderItem.Order = order;

                    await dbContext.AddAsync(orderItem);


                    await dbContext.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception e)
            {

                return false;
            }

        }
        public override async Task<bool> UpdateAsync(OrderItem orderItem)
        {
            if (orderItem?.Order == null)
            {
                return false;
            }
            if (orderItem?.Product == null)
            {
                return false;
            }

            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                   .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {

                    var existingEntity = dbContext.Set<OrderItem>().FirstOrDefault(p => p.Id == orderItem.Id);
                    if (existingEntity == null)
                    {
                        return false;
                    }

                    var order = dbContext.Set<Order>().FirstOrDefault(o => o.Id == orderItem.Order.Id);
                    if (order == null)
                    {
                        return false;
                    }

                    var product = dbContext.Set<Product>().FirstOrDefault(p => p.Name == orderItem.Product.Name);
                    if (order == null)
                    {
                        return false;
                    }

                    orderItem.ProductId = product?.Id;
                    existingEntity.UpdateProperties(orderItem);

                    dbContext.Update(existingEntity);

                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public override async Task<List<OrderItem>> GetAllAsync()
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                   .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    List<OrderItem> entityList = await dbContext.Set<OrderItem>()
                        .Include(o => o.Order)
                        .Include(o => o.Product)
                        .ToListAsync();

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
                    var entity = await dbContext.Set<OrderItem>().FirstOrDefaultAsync(o => o.Id == id);

                    if (entity == null)
                    {
                        return false;
                    }

                    dbContext.Set<OrderItem>().Remove(entity);
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
                    var entities = await dbContext.Set<OrderItem>().Where(e => ids.Contains(e.Id)).ToListAsync();
                    if (entities == null)
                    {
                        return false;
                    }

                    //foreach (var item in entities)
                    //{
                    //    dbContext.Set<OrderItem>().Remove(item);
                    //}
                    dbContext.Set<OrderItem>().RemoveRange(entities);

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
