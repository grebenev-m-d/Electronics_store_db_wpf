using System.Collections.Generic;

#nullable disable

namespace Electronics_store_db_wpf.Data.DatabaseModel
{
    public partial class Category : BaseEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
