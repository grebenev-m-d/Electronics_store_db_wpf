using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electronics_store_db_wpf.MVVM.Model
{
    public class FilterOrderItems
    {
        public int MinQuantity { get; set; }
        public int MaxQuantity { get; set; }
        public string ProductName { get; set; }
    }
}
