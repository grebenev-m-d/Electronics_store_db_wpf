using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.EFcommonRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electronics_store_db_wpf.Data.Repository.ClientRepository
{
    public interface IClientRepository : IEFcommonRepository<Client>
    {
        public Task<Client> GetByIdIncludeOrdersAsync(Guid id);
        public Task<List<Client>> GetAllClientsWithOrdersAndItemsAndProductsAsync();
    }
}
