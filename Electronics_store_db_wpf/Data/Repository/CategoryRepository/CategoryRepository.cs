using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.EFcommonRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Electronics_store_db_wpf.Data.Repository.CategoryRepository
{
    public class CategoryRepository : EFcommonRepository<Category>, ICategoryRepository
    {
        private readonly IServiceProvider serviceProvider;


        public CategoryRepository(IServiceProvider _serviceProvider) : base(_serviceProvider)
        {
            serviceProvider = _serviceProvider;

        }
        
        public async Task<List<Category>> GetAllIncludeProductAsync()
        {
            
            try
            {
                using (var dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                   
                    List<Category> entityList = await dbContext.Categories
                    .Include(u => u.Products).ToListAsync();

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
