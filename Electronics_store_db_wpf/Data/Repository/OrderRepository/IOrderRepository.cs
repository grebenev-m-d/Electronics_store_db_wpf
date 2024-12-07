using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.EFcommonRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electronics_store_db_wpf.Data.Repository.OrderRepository
{
    public interface IOrderRepository : IEFcommonRepository<Order>
    {
        public Task<List<Order>> GetByClientIdAsync(Guid orderId);
    }
}
