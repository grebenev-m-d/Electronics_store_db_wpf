using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.EFcommonRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electronics_store_db_wpf.Data.Repository.ClientRepository
{
    public class ClientRepository : EFcommonRepository<Client>, IClientRepository
    {

        private readonly IServiceProvider serviceProvider;
        public ClientRepository(IServiceProvider _serviceProvider) : base(_serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }
        
        public async Task<Client> GetByIdIncludeOrdersAsync(Guid id)
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                  .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                       var entity = await dbContext.Set<Client>()
                        .Include(c => c.Orders).ThenInclude(o => o.OrderItems)
                        .FirstOrDefaultAsync(p => p.Id == id);
                       
                    return entity;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public async Task<List<Client>> GetAllClientsWithOrdersAndItemsAndProductsAsync()
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope()
                  .ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    var entity = await dbContext.Set<Client>()
                     .Include(x => x.Orders)
                     .ThenInclude(x => x.OrderItems)
                     .ThenInclude(x => x.Product)
                     .ToListAsync();

                    return entity;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
