using System;
using System.Collections.Generic;

#nullable disable

namespace Electronics_store_db_wpf.Data.DatabaseModel
{
    public partial class Client : BaseEntity
    {
        public Client()
        {
            Orders = new HashSet<Order>();
        }

        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? Birthday { get; set; }
        public string Passwordhash { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
