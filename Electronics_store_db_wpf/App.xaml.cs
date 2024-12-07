using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Electronics_store_db_wpf.Core;
using Electronics_store_db_wpf.Data;
using Electronics_store_db_wpf.MVVM.ViewModel;
using Electronics_store_db_wpf.Data.Repository.CategoryRepository;
using Electronics_store_db_wpf.Data.Repository.ProductRepository;
using System;
using System.Windows;
using Electronics_store_db_wpf.Data.Repository.CurrentEmployeeRepository;
using Electronics_store_db_wpf.Data.Repository.EmployeeRepository;
using Electronics_store_db_wpf.Data.Repository.ClientRepository;
using Electronics_store_db_wpf.Data.Repository.OrderRepository;
using Electronics_store_db_wpf.Data.Repository.OrderItemRepository;
using Electronics_store_db_wpf.Data.Repository.RoleRepository;
using Electronics_store_db_wpf.Services.ExcelWriterService;
using OfficeOpenXml;
using Electronics_store_db_wpf.Services.NavigationService;
using System.IO;
using Microsoft.Extensions.Configuration;
using Electronics_store_db_wpf.Helper;

namespace Electronics_store_db_wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
           
            var configurationBuilder = new ConfigurationBuilder();

            string filePath = Path.Combine(FilePathHelper._baseDirectory, "appsettings.json");
            configurationBuilder.AddJsonFile(filePath);

            var configuration = configurationBuilder.Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");



            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<MainWindowView>(provider=> new MainWindowView
            {
                DataContext = provider.GetRequiredService<MainWindowViewModel>()
            });
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainContentViewModel>();
            services.AddSingleton<ProductsViewModel>();
            services.AddSingleton<ClientViewModel>();
            services.AddSingleton<EmployeeViewModel>();
            services.AddSingleton<OrdersViewModel>();
            services.AddSingleton<DocumentManagerViewModel>();  

            services.AddScoped<ElektonikaDbContext>(provider =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<ElektonikaDbContext>();
                optionsBuilder.UseSqlServer(connectionString);
                return new ElektonikaDbContext(optionsBuilder.Options, connectionString);
            });

            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<IClientRepository, ClientRepository>();
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<IOrderItemRepository, OrderItemRepository>();
            services.AddSingleton<IRoleRepository, RoleRepository>();


            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            services.AddSingleton<IExcelWriterService>(new ExcelWriterService(@$"{desktopPath}\store_doc"));


            services.AddSingleton< ICurrentEmployeeRepository,CurrentEmployeeRepository> ();

            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();

        }

        protected override void OnStartup(StartupEventArgs e)
        {

            var mainWindow = _serviceProvider.GetRequiredService<MainWindowView>();
            var navigationService = _serviceProvider.GetRequiredService<INavigationService>();
            navigationService.NavigateToMainWindow<LoginViewModel>(); // Открываем LoginView
            mainWindow.Show();
            base.OnStartup(e);

        }
    }
}
