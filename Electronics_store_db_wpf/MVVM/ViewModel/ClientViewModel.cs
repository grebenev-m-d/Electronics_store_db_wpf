using Electronics_store_db_wpf.Core;
using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.ClientRepository;
using Electronics_store_db_wpf.Helper;
using Electronics_store_db_wpf.Helper.ValidationRule;
using Electronics_store_db_wpf.MVVM.Model;
using Electronics_store_db_wpf.Services.NavigationService;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Electronics_store_db_wpf.MVVM.ViewModel
{
    public class ClientViewModel : Core.ViewModel
    {
        private readonly IClientRepository _clientRepository;

        public RelayCommand UpdateDataCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand FilterCommand { get; set; }
        public RelayCommand AddingNewItemToDgClientCommand { get; set; }

        private INavigationService _navigation;
        public INavigationService Navigation
        {
            get { return _navigation; }
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }



        private ObservableCollection<WrapperObject<Client>> _clients;
        public ObservableCollection<WrapperObject<Client>> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                OnPropertyChanged();
            }
        }


        private WrapperObject<Client> _selectedClient;
        public WrapperObject<Client> SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                if (_selectedClient?.DatabaseModel != null && PropertyValidator.Client(_selectedClient.DatabaseModel))
                {
                    _selectedClient.ErrorValidation.HasError = false;
                    AddOrUpdate(_selectedClient.DatabaseModel);
                }
                else if (_selectedClient?.DatabaseModel != null)
                {
                    _selectedClient.ErrorValidation.HasError = true;
                }
                _selectedClient = value;
                OnPropertyChanged();

            }
        }

        private FilterClients _filterOptions;
        public FilterClients FilterOptions
        {
            get { return _filterOptions; }
            set
            {
                if (value != null)
                {
                    _filterOptions = value;
                    OnPropertyChanged();
                }
            }
        }

        public ClientViewModel(INavigationService navigation, IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
            Navigation = navigation;
            FilterCommand = new RelayCommand(FilterItems, CanFilterItems);
            DeleteCommand = new RelayCommand(DeleteItems, CanDeleteItems);

            UpdateDataCommand = new RelayCommand(UpdateData, CanUpdateData);
            AddingNewItemToDgClientCommand = new RelayCommand(AddingNewItemToDgClient, CanAddingNewItemToDgClient);

              _ = Initiaize();
        }
        private async Task Initiaize()
        {
            FilterOptions = new FilterClients();
            Clients = new ObservableCollection<WrapperObject<Client>>(WrapperObject<Client>.WrapList(await _clientRepository.GetAllAsync()));
        }
        private void UpdateData(object obj)
        {
            _ = Initiaize();
        }
        private bool CanUpdateData(object obj) => true;

        private async void FilterItems(object parameter)
        {
            if (FilterOptions == null) { return; }

            List<Client> _clients = await _clientRepository.GetAllAsync();

            Clients = new ObservableCollection<WrapperObject<Client>>(WrapperObject<Client>.WrapList(FilterHelper.FilterClients(_clients, FilterOptions)));

        }
        private bool CanFilterItems(object parameter) => true;

        private void AddingNewItemToDgClient(object parameter)
        {
            if (parameter is AddingNewItemEventArgs args)
            {
                args.NewItem = new WrapperObject<Client>() { DatabaseModel = new Client() };
            }
        }
        private bool CanAddingNewItemToDgClient(object parameter) => true;

        private async void DeleteItems(object parameter)
        {


            if (parameter == null)
            {
                return;
            }

            var removedProducts = ((IList)parameter)?.OfType<Client>().ToList();

            if (removedProducts == null)
            {
                return;
            }
            if (Clients == null)
            {
                return;
            }
            foreach (var item in removedProducts)
            {
                var productToRemove = Clients.FirstOrDefault(p => p.DatabaseModel.Id == item.Id);
                if (productToRemove != null)
                {
                    Clients.Remove(productToRemove);
                }
            }

            Clients = Clients;


               await _clientRepository.DeleteByIdsAsync(removedProducts.Select(p => p.Id).ToList());


        }
        private bool CanDeleteItems(object parameter) => parameter != null;


        private async void AddOrUpdate(Client _selectedClient)
        {

            if (_selectedClient != null)
            {

                var exists = await _clientRepository.CheckRecordByIdAsync(_selectedClient.Id);

                if (exists)
                {
                    await _clientRepository.UpdateAsync(_selectedClient);
                }
                else
                {
                    await _clientRepository.AddAsync(_selectedClient);
                }
            }

        }

    }
}
