using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.EFcommonRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electronics_store_db_wpf.Data.Repository.OrderItemRepository
{
    public interface IOrderItemRepository : IEFcommonRepository<OrderItem>
    {
        public Task<List<OrderItem>> GetItemsByOrderIdIncludingProductsAsync(Guid orderId);
    }
}
