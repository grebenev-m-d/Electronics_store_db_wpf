using Electronics_store_db_wpf.Core;
using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.CurrentEmployeeRepository;
using Electronics_store_db_wpf.Services.NavigationService;

namespace Electronics_store_db_wpf.MVVM.ViewModel
{
    public class MainContentViewModel : Core.ViewModel
    {
        private INavigationService _navigation;
        private ICurrentEmployeeRepository _currentEmployee;
        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }
        public Employee CurrentEmployee
        {
            get{
                return _currentEmployee.CurrentEmployee;
            }
            set
            {
                _currentEmployee.CurrentEmployee = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand NavigateToProductsCommand { get; set; }
        public RelayCommand NavigateToClientsCommand { get; set; }
        public RelayCommand NavigateToEmployeesCommand { get; set; }
        public RelayCommand NavigateToOrdersCommand { get; set; }
        public RelayCommand NavigateToDocumentManagerCommand { get; set; }
        public RelayCommand NavigateToExitCommand { get; set; }


        public MainContentViewModel(INavigationService navService, ICurrentEmployeeRepository currentEmployee) 
        {
            Navigation = navService;
            _currentEmployee = currentEmployee;

            NavigateToProductsCommand = new RelayCommand(obj => { Navigation.NavigateToMainContent<ProductsViewModel>(); }, obj => true);
            NavigateToClientsCommand = new RelayCommand(obj => { Navigation.NavigateToMainContent<ClientViewModel>(); }, obj => true);
            NavigateToEmployeesCommand = new RelayCommand(obj => { Navigation.NavigateToMainContent<EmployeeViewModel>(); }, obj => true);
            NavigateToOrdersCommand = new RelayCommand(obj => { Navigation.NavigateToMainContent<OrdersViewModel>(); }, obj => true);
            NavigateToDocumentManagerCommand = new RelayCommand(obj => { Navigation.NavigateToMainContent<DocumentManagerViewModel>(); }, obj => true);
            NavigateToExitCommand = new RelayCommand(obj => { Navigation.NavigateToMainWindow<LoginViewModel>(); }, obj => true);
        }
    }
}
