using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electronics_store_db_wpf.MVVM.Model
{
    public class FilterProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal MinPrice { get; set; }  
        public decimal MaxPrice { get; set; }
    }
}
