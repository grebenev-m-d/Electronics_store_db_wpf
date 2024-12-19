using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.EFcommonRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electronics_store_db_wpf.Data.Repository.CategoryRepository
{
    public interface ICategoryRepository : IEFcommonRepository<Category>
    {
        public Task<List<Category>> GetAllIncludeProductAsync();
    }
}
