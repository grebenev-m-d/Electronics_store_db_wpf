using Microsoft.VisualBasic;
using OfficeOpenXml.Style;
using Electronics_store_db_wpf.Core;
using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.ClientRepository;
using Electronics_store_db_wpf.Data.Repository.EmployeeRepository;
using Electronics_store_db_wpf.Data.Repository.OrderItemRepository;
using Electronics_store_db_wpf.Data.Repository.OrderRepository;
using Electronics_store_db_wpf.Data.Repository.ProductRepository;
using Electronics_store_db_wpf.Helper;
using Electronics_store_db_wpf.Helper.ValidationRule;
using Electronics_store_db_wpf.MVVM.Model;
using Electronics_store_db_wpf.Services;
using Electronics_store_db_wpf.Services.NavigationService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Electronics_store_db_wpf.MVVM.ViewModel
{
    public class OrdersViewModel : Core.ViewModel
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;

        private INavigationService _navigation;

        public RelayCommand UpdateDataCommand { get; set; }


        //dgClients
        public RelayCommand FilterToDgClientsCommand { get; set; }

        //dgOrders
        public RelayCommand DeleteToDgOrdersCommand { get; set; }
        public RelayCommand FilterToDgOrdersCommand { get; set; }
        public RelayCommand CellEditEndingToDgOrdersCommand { get; set; }
        public RelayCommand AddingNewItemToDgOrdersCommand { get; set; }

        //dgOrderItems
        public RelayCommand DeleteToDgOrderItemsCommand { get; set; }
        public RelayCommand FilterToDgOrderItemsCommand { get; set; }
        public RelayCommand AddingNewItemToDgOrderItemsCommand { get; set; }

      
        public INavigationService Navigation
        {
            get { return _navigation; }
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        private List<Product> _productNamesList;
        public List<Product> ProductNamesList

        {
            get { return _productNamesList; }
            set
            {
                _productNamesList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Client> _clientsCollection;
        public ObservableCollection<Client> ClientsCollection
        {
            get { return _clientsCollection; }
            set
            {
                _clientsCollection = value;
                OnPropertyChanged();
            }
        }

        private Client _selectedClient;
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
               
                _selectedClient = value;
                OnPropertyChanged();

                LoadOrders(_selectedClient);
            }
        }

        private FilterClients _filterOptionsClients;
        public FilterClients FilterOptionsClients
        {
            get { return _filterOptionsClients; }
            set
            {
                if (value != null)
                {
                    _filterOptionsClients = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<WrapperObject<Order>> _ordersCollection;
        public ObservableCollection<WrapperObject<Order>> OrdersCollection
        {
            get { return _ordersCollection; }
            set
            {
                _ordersCollection = value;
                OnPropertyChanged();
            }
        }

        private WrapperObject<Order> _selectedOrder;
        public WrapperObject<Order> SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;
                if (value != null)
                {
                    _selectedOrder.DatabaseModel.Client = SelectedClient;
                }

                if (_selectedOrder?.DatabaseModel != null && PropertyValidator.Order(_selectedOrder.DatabaseModel))
                {
                  
                    _selectedOrder.ErrorValidation.HasError = false;
                    AddOrUpdateOrder(_selectedOrder.DatabaseModel);
                }
                else if (_selectedOrder?.DatabaseModel != null)
                {
                    _selectedOrder.ErrorValidation.HasError = true;
                }

               
                OnPropertyChanged();
                if (_selectedOrder != null)
                {
                    LoadOrderItems(_selectedOrder.DatabaseModel);
                }
            }
        }

        private FilterOrders _filterOptionsOrders;
        public FilterOrders FilterOptionsOrders
        {
            get { return _filterOptionsOrders; }
            set
            {
                if (value != null)
                {
                    _filterOptionsOrders = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<WrapperObject<OrderItem>> _OrderItemsCollection;
        public ObservableCollection<WrapperObject<OrderItem>> OrderItemsCollection
        {
            get { return _OrderItemsCollection; }
            set
            {
                _OrderItemsCollection = value;
                OnPropertyChanged(nameof(OrderItemsCollection));
            }
        }

        private WrapperObject<OrderItem> _selectedOrderItem;
        public WrapperObject<OrderItem> SelectedOrderItem
        {
            get { return _selectedOrderItem; }
            set
            {
                CalculateOrderAmount();
                if (_selectedOrderItem?.DatabaseModel != null && PropertyValidator.OrderItem(_selectedOrderItem.DatabaseModel))
                {
                  
                    _selectedOrderItem.ErrorValidation.HasError = false;
                    AddOrUpdateOrderItem(_selectedOrderItem.DatabaseModel);
                }
                else if (_selectedOrderItem?.DatabaseModel != null)
                {
                    _selectedOrderItem.ErrorValidation.HasError = true;
                }

                _selectedOrderItem = value;
                if (_selectedOrderItem?.DatabaseModel != null && SelectedOrder?.DatabaseModel!=null)
                {
                    _selectedOrderItem.DatabaseModel.Order = SelectedOrder.DatabaseModel;
                }
                OnPropertyChanged();
            }
        }

        private FilterOrderItems _filterOptionsOrderItems;
        public FilterOrderItems FilterOptionsOrderItems
        {
            get { return _filterOptionsOrderItems; }
            set
            {
                if (value != null)
                {
                    _filterOptionsOrderItems = value;
                    OnPropertyChanged();
                }
            }
        }

        public OrdersViewModel(INavigationService navigation,
            IClientRepository clientRepository, 
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;

            Navigation = navigation;

            FilterToDgClientsCommand = new RelayCommand(FiltrationToDgClientsAsync, CanFiltrationToDgClients);

            //dgOrders
            DeleteToDgOrdersCommand = new RelayCommand(DeleteToDgOrders, CanDeleteToDgOrders);
            FilterToDgOrdersCommand = new RelayCommand(FiltrationToDgOrdersAsync, CanFiltrationToDgOrders);
            AddingNewItemToDgOrdersCommand = new RelayCommand(AddingNewItemToDgOrders, CanAddingNewItemToDgOrders);

            //dgOrderItems
            DeleteToDgOrderItemsCommand = new RelayCommand(DeleteToDgOrderItems, CanDeleteToDgOrderItems);
            FilterToDgOrderItemsCommand = new RelayCommand(FiltrationToDgOrderItemsAsync, CanFiltrationToDgOrderItems);
            AddingNewItemToDgOrderItemsCommand = new RelayCommand(AddingNewItemToDgOrderItems, CanAddingNewItemToDgOrderItems);

            UpdateDataCommand = new RelayCommand(UpdateData, CanUpdateData);


            _ = Initiaize();
        }
        private async Task Initiaize()
        {
            FilterOptionsClients = new FilterClients();
            FilterOptionsOrders = new FilterOrders();
            FilterOptionsOrderItems = new FilterOrderItems();

            ClientsCollection = new ObservableCollection<Client>(await _clientRepository.GetAllAsync());

            ProductNamesList = new List<Product>((await _productRepository.GetAllAsync()));
        }



        private void UpdateData(object obj)
        {
            _ = Initiaize();
        }
        private bool CanUpdateData(object obj) => true;



        //dgClients
        private async void FiltrationToDgClientsAsync(object parameter)
        {
            if (FilterOptionsOrderItems == null) { return; }

            List<Client> orderItems = await _clientRepository.GetAllAsync();

            if (orderItems == null || orderItems.Count < 2)
            {
                return;
            }

            ClientsCollection = new ObservableCollection<Client>(FilterHelper.FilterClients(orderItems, FilterOptionsClients));
        }
        private bool CanFiltrationToDgClients(object parameter) => true;
        //dgOrders
        private async void DeleteToDgOrders(object parameter)
        {
            if (parameter == null)
            {
                return;
            }

            var remoteCollection = ((IList)parameter)?.OfType<WrapperObject<Order>>().ToList();

            if (remoteCollection == null)
            {
                return;
            }
            if (OrdersCollection == null)
            {
                return;
            }

            foreach (var item in remoteCollection)
            {
                var productToRemove = OrdersCollection.FirstOrDefault(p => p.DatabaseModel.Id == item.DatabaseModel.Id);
                if (productToRemove != null)
                {
                    OrdersCollection.Remove(productToRemove);
                }
            }

            OrdersCollection = OrdersCollection;


               await _orderRepository.DeleteByIdsAsync(remoteCollection.Select(p => p.DatabaseModel.Id).ToList());

        }
        private bool CanDeleteToDgOrders(object parameter) => parameter != null;

        private async void FiltrationToDgOrdersAsync(object parameter)
        {
            if (FilterOptionsOrders == null || SelectedClient == null) { return; }

            List<Order> orders = await _orderRepository.GetByClientIdAsync(SelectedClient.Id);

            if (orders == null || orders.Count < 2)
            {
                return;
            }

            OrdersCollection = new ObservableCollection<WrapperObject<Order>>(WrapperObject<Order>.WrapList(FilterHelper.FilterOrders(orders, FilterOptionsOrders)));
        }
        private bool CanFiltrationToDgOrders(object parameter) => true;

        private void AddingNewItemToDgOrders(object parameter)
        {
            if (parameter is AddingNewItemEventArgs args)
            {
                args.NewItem = new WrapperObject<Order>() { DatabaseModel = new Order { OrderDate = DateTime.Now } };
            }
        }
        private bool CanAddingNewItemToDgOrders(object parameter) => true;


        //dgOrderItems
        private async void DeleteToDgOrderItems(object parameter)
        {
            if (parameter == null)
            {
                return;
            }

            var remoteCollection = ((IList)parameter)?.OfType<WrapperObject<OrderItem>>().ToList();

            if (remoteCollection == null)
            {
                return;
            }
            if (OrdersCollection == null)
            {
                return;
            }

            foreach (var item in remoteCollection)
            {
                var productToRemove = OrderItemsCollection.FirstOrDefault(p => p.DatabaseModel.Id == item.DatabaseModel.Id);
                if (productToRemove != null)
                {
                    OrderItemsCollection.Remove(productToRemove);
                }
            }
            await _orderItemRepository.DeleteByIdsAsync(remoteCollection.Select(p => p.DatabaseModel.Id).ToList());
        }
        private bool CanDeleteToDgOrderItems(object parameter) => parameter != null;

        private async void FiltrationToDgOrderItemsAsync(object parameter)
        {
            if (FilterOptionsOrderItems == null || SelectedOrder == null) { return; }

            List<OrderItem> orderItems = await _orderItemRepository.GetItemsByOrderIdIncludingProductsAsync(SelectedOrder.DatabaseModel.Id);

            if (orderItems == null || orderItems.Count < 2)
            {
                return;
            }

            OrderItemsCollection = new ObservableCollection<WrapperObject<OrderItem>>(WrapperObject<OrderItem>.WrapList(FilterHelper.FilterOrderItems(orderItems, FilterOptionsOrderItems)));
        }
        private bool CanFiltrationToDgOrderItems(object parameter) => true;

        private void AddingNewItemToDgOrderItems(object parameter)
        {
            if (parameter is AddingNewItemEventArgs args)
            {
                args.NewItem = new WrapperObject<OrderItem>() { DatabaseModel = new OrderItem { Product = new Product(), Order = SelectedOrder.DatabaseModel } };
            
            }
        }
        private bool CanAddingNewItemToDgOrderItems(object parameter) => true;



        //Secondary functions
        private void CalculateOrderAmount()
        {
            if (SelectedOrderItem?.DatabaseModel == null || SelectedOrder?.DatabaseModel == null)
            {
                return;
            }
            if (SelectedOrderItem.DatabaseModel.Quantity.HasValue && SelectedOrderItem.DatabaseModel.Product.Price.HasValue)
            {
                SelectedOrderItem.DatabaseModel.Amount = SelectedOrderItem.DatabaseModel.Quantity * SelectedOrderItem.DatabaseModel.Product.Price;
                SelectedOrder.DatabaseModel.TotalAmount = OrderItemsCollection.Sum(oi => oi.DatabaseModel.Amount);
            }
        }

        private async void LoadOrders(Client client)
        {
            if (client == null)
            {
                OrdersCollection = null;
                return;
            }
            
            var orders = (await _clientRepository.GetByIdIncludeOrdersAsync(client.Id)).Orders.ToList();
            OrdersCollection = new ObservableCollection<WrapperObject<Order>>(WrapperObject<Order>.WrapList(orders));
            OrderItemsCollection = null;

        }

        private async void LoadOrderItems(Order order)
        {
            if (order == null)
            {
                OrderItemsCollection = null;
                return;
            }
            var _orderItems = await _orderItemRepository.GetItemsByOrderIdIncludingProductsAsync(order.Id);

            if (_orderItems == null)
            {
                OrderItemsCollection = new ObservableCollection<WrapperObject<OrderItem>>();
                return;
            }

            OrderItemsCollection = new ObservableCollection<WrapperObject<OrderItem>>((WrapperObject<OrderItem>.WrapList(_orderItems)));
        }
        private async void AddOrUpdateOrder(Order order)
        {
          
            if (order != null)
            {
                var exists = await _orderRepository.CheckRecordByIdAsync(order.Id);

                if (exists)
                {
                    await _orderRepository.UpdateAsync(order);
                }
                else
                {
                    await _orderRepository.AddAsync(order);
                }
            }

        }
        private async void AddOrUpdateOrderItem(OrderItem orderItem)
        {

            if (orderItem != null)
            {
                var exists = await _orderItemRepository.CheckRecordByIdAsync(orderItem.Id);

                if (exists)
                {
                    await _orderItemRepository.UpdateAsync(orderItem);
                }
                else
                {
                    await _orderItemRepository.AddAsync(orderItem);
                }
            }

        }
    }
}
