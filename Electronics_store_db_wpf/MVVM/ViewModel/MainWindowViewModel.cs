using Electronics_store_db_wpf.Core;
using Electronics_store_db_wpf.Services.NavigationService;

namespace Electronics_store_db_wpf.MVVM.ViewModel
{
    public class MainWindowViewModel : Core.ViewModel
    {
        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand NavigateToLoginCommand { get; set; }
        public MainWindowViewModel(INavigationService navService) 
        {
            Navigation = navService;

            NavigateToLoginCommand = new RelayCommand(obj => { Navigation.NavigateToMainWindow<LoginViewModel>(); }, obj => true);
        }

    }
}
