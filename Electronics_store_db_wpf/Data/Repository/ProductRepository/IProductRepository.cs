using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.EFcommonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electronics_store_db_wpf.Data.Repository.ProductRepository
{
    public interface IProductRepository : IEFcommonRepository<Product>
    {
        public Task<List<Product>> GetProductsInCategoryAsync(string nameCategory);

    }
}
