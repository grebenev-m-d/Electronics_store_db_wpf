using Electronics_store_db_wpf.Core;
using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.ClientRepository;
using Electronics_store_db_wpf.Data.Repository.ProductRepository;
using Electronics_store_db_wpf.Services.ExcelWriterService;
using System.Collections.Generic;


namespace Electronics_store_db_wpf.MVVM.ViewModel
{
    public class DocumentManagerViewModel : Core.ViewModel
    {
        private readonly IExcelWriterService _excelWriterService;
        private readonly IProductRepository _productRepository;
        private readonly IClientRepository _clientRepository;
        public RelayCommand CreateInvoiceCommand { get; set; }
        public RelayCommand CreateCustomerReportCommand { get; set; }
        public DocumentManagerViewModel(IExcelWriterService excelWriterService,
            IProductRepository productRepository,
            IClientRepository clientRepository)
        {
            _excelWriterService = excelWriterService;
            _productRepository = productRepository;
            _clientRepository = clientRepository;

            CreateInvoiceCommand = new RelayCommand(CreateInvoice, obj => true);
            CreateCustomerReportCommand = new RelayCommand(CreateCustomerReport, obj => true);
        }
        public async void CreateInvoice (object parameter)
        {
            List<Product> products = await _productRepository.GetAllAsync();

           await _excelWriterService.CreateInvoiceAsync(products);
        }
        public async void CreateCustomerReport(object parameter)
        {
            List<Client> products = await _clientRepository.GetAllClientsWithOrdersAndItemsAndProductsAsync();

            await _excelWriterService.CreateCustomerReportAsync(products);
        }
    }
}
