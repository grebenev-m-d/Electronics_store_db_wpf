using Microsoft.Win32;
using Electronics_store_db_wpf.Core;
using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.CategoryRepository;
using Electronics_store_db_wpf.Data.Repository.ProductRepository;
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
    public class ProductsViewModel : Core.ViewModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public RelayCommand ChangeImageCommand { get; set; }
        public RelayCommand CreateExcelDocCommand { get; set; }
        public RelayCommand UpdateDataCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand FilterCommand { get; set; }
        public RelayCommand AddingNewItemToDgProductsCommand { get; set; }



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

        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
                LoadProductsByCategory(_selectedCategory);
            }
        }
        private ObservableCollection<WrapperObject<Product>> _products;
        public ObservableCollection<WrapperObject<Product>> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }


        private WrapperObject<Product> _selectedProduct;
        public WrapperObject<Product> SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {

                if (_selectedProduct?.DatabaseModel != null && PropertyValidator.Product(_selectedProduct.DatabaseModel))
                {
                    _selectedProduct.ErrorValidation.HasError = false;
                    AddOrUpdate(_selectedProduct.DatabaseModel);
                }
                else if (_selectedProduct?.DatabaseModel != null)
                {
                    _selectedProduct.ErrorValidation.HasError = true;
                }

                _selectedProduct = value;
                if (value != null)
                {
                    _selectedProduct.DatabaseModel.Category = SelectedCategory;
                }
                OnPropertyChanged();



            }
        }

        private FilterProduct _filterOptions;
        public FilterProduct FilterOptions
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


        public ProductsViewModel(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository
            )
        {

            _productRepository = productRepository;
            _categoryRepository = categoryRepository;

            FilterCommand = new RelayCommand(FilterItems, CanFilterItems);
            DeleteCommand = new RelayCommand(DeleteItems, CanDeleteItems);
            AddingNewItemToDgProductsCommand = new RelayCommand(AddingNewItemToDgProducts, CanAddingNewItemToDgProducts);

            ChangeImageCommand = new RelayCommand(ChangeImage, CanChangeImage);
            UpdateDataCommand = new RelayCommand(UpdateData, CanUpdateData);

            _ = Initiaize();
        }
        private async Task Initiaize()
        {
            _filterOptions = new FilterProduct();
            Categories = new ObservableCollection<Category>(await _categoryRepository.GetAllIncludeProductAsync());
            Products = new ObservableCollection<WrapperObject<Product>>();
        }


        private void UpdateData(object parameter)
        {
            _ = Initiaize();
        }
        private bool CanUpdateData(object parameter) => true;





        private void AddingNewItemToDgProducts(object parameter)
        {
            if (parameter is AddingNewItemEventArgs args)
            {
                args.NewItem = new WrapperObject<Product>() { DatabaseModel = new Product() };
            }
        }
        private bool CanAddingNewItemToDgProducts(object parameter) => true;
        private async void FilterItems(object parameter)
        {
            if (FilterOptions == null || SelectedCategory == null) { return; }

            List<Product> newProducts = await _productRepository.GetProductsInCategoryAsync(SelectedCategory.Name);

            Products = new ObservableCollection<WrapperObject<Product>>(WrapperObject<Product>.WrapList(FilterHelper.FilterProducts(newProducts, FilterOptions)));

        }
        private bool CanFilterItems(object parameter) => true;

        private async void DeleteItems(object parameter)
        {


            if (parameter == null)
            {
                return;
            }

            var removedProducts = ((IList)parameter)?.OfType<WrapperObject<Product>>().ToList();

            if (removedProducts == null)
            {
                return;
            }
            if (Products == null)
            {
                return;
            }
            foreach (var item in removedProducts)
            {
                var productToRemove = Products.FirstOrDefault(p => p.DatabaseModel.Id == item.DatabaseModel.Id);
                if (productToRemove != null)
                {
                    Products.Remove(productToRemove);
                }
            }

            Products = Products;


            await _productRepository.DeleteByIdsAsync(removedProducts.Select(p => p.DatabaseModel.Id).ToList());


        }
        private bool CanDeleteItems(object parameter) => true;

        private void ChangeImage(object parameter)
        {
            if (SelectedProduct != null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif) | *.jpg; *.jpeg; *.png; *.gif";

                if (openFileDialog.ShowDialog() == true)
                {
                    string imagePath = openFileDialog.FileName;
                    SelectedProduct.DatabaseModel.Image = imagePath;
                    SelectedProduct = SelectedProduct;
                }
            }
        }
        private bool CanChangeImage(object parameter) => SelectedProduct != null;



        private async void LoadProductsByCategory(Category category)
        {
            if (category != null)
            {
                Products = new ObservableCollection<WrapperObject<Product>>
                    (WrapperObject<Product>.WrapList(await _productRepository.GetProductsInCategoryAsync(category.Name)));
            }
            else
            {
                Products = null;
            }
        }

        private async void AddOrUpdate(Product _selectedProduct)
        {

            if (_selectedProduct != null)
            {

                var exists = await _productRepository.CheckRecordByIdAsync(_selectedProduct.Id);

                if (exists)
                {
                    await _productRepository.UpdateAsync(_selectedProduct);
                }
                else
                {
                    await _productRepository.AddAsync(_selectedProduct);
                }
            }

        }
    }
}
