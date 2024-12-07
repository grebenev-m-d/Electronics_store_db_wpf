using System;
using System.Collections.Generic;

#nullable disable

namespace Electronics_store_db_wpf.Data.DatabaseModel
{
    public partial class Product : BaseEntity
    {

        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }

        public string Description { get; set; }
        public int? Quantity { get; set; }
        public Guid? CategoryId { get; set; }
     

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }


        public void UpdateProperties(Product updatedProduct)
        {
           
            // Проверяем, есть ли обновления для каждого поля и обновляем их, если есть
            if (updatedProduct.Name != null)
            {
                this.Name = updatedProduct.Name;
            }
            if (updatedProduct.Price != null)
            {
                this.Price = updatedProduct.Price;
            }
            if (updatedProduct.Image != null)
            {
                this.Image = updatedProduct.Image;
            }
            if (updatedProduct.Description != null)
            {
                this.Description = updatedProduct.Description;
            }
            if (updatedProduct.Quantity != null)
            {
                this.Quantity = updatedProduct.Quantity;
            }
            if (updatedProduct.CategoryId.HasValue)
            {
                this.CategoryId = updatedProduct.CategoryId.Value;
            }
        }

    }
}
