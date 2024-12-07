using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.EFcommonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Electronics_store_db_wpf.Data.Repository.ProductRepository
{
    public class ProductRepository : EFcommonRepository<Product>, IProductRepository
    {
        private readonly IServiceProvider serviceProvider;
        public ProductRepository(IServiceProvider _serviceProvider) : base(_serviceProvider)
        {
            serviceProvider = _serviceProvider;

        }


        public async Task<List<Product>> GetProductsInCategoryAsync(string nameCategory)
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
               

                    var category = await dbContext.Categories.Include(u => u.Products).FirstOrDefaultAsync(p => p.Name == nameCategory);

                    if (category == null) { return null; }

                    List<Product> products =  category.Products.ToList();

                    return products;
                }
            
            }
            catch (Exception e)
            {

                return null;
            }

        }


   
        public override async Task<bool> UpdateAsync(Product product)
        {
            if (product?.Category?.Name == null)
            {
                return false;
            }

            try
            {
                using (var dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                  

                    var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Name == product.Category.Name);
                    if (category == null || category?.Id == null)
                    {
                        return false;
                    }
                    product.CategoryId = category.Id;

                    var existingProduct = await dbContext.Products.FindAsync(product.Id);
                    if (existingProduct == null)
                    {
                        return false;
                    }
                    existingProduct.UpdateProperties(product);
                    dbContext.Update(existingProduct);
                    await dbContext.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }



        public async override Task<bool> AddAsync(Product product)
        {
            if (product?.Category?.Name == null)
            {
                return false;
            }
            try
            {
               
                using (var dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    // Проверяем, существует ли категория в базе данных
                    var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Name == product.Category.Name);

                    // Если категория не найдена, возвращаем false
                    if (category == null)
                    {
                        return false;
                    }

                    product.Category = category;

                    dbContext.Products.Add(product);
                    await dbContext.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception e)
            {
                // Обработка ошибок, если возникла
                return false;
            }
        }

    }
}
