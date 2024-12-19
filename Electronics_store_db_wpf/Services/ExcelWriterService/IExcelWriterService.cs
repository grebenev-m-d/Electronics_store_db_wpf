using Electronics_store_db_wpf.Data.DatabaseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electronics_store_db_wpf.Services.ExcelWriterService
{
    public interface IExcelWriterService
    {
        public Task CreateInvoiceAsync(List<Product> products);
        public Task CreateCustomerReportAsync(List<Client> products);
    }
}
