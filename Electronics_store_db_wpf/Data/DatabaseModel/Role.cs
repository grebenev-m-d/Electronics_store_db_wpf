using System.Collections.Generic;

#nullable disable

namespace Electronics_store_db_wpf.Data.DatabaseModel
{
    public partial class Role : BaseEntity
    {
        public Role()
        {
            Employees = new HashSet<Employee>();
        }

        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
