using Electronics_store_db_wpf.Core;
using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.EmployeeRepository;
using Electronics_store_db_wpf.Data.Repository.RoleRepository;
using Electronics_store_db_wpf.Helper;
using Electronics_store_db_wpf.MVVM.Model;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Electronics_store_db_wpf.Helper.ValidationRule;
using Microsoft.Win32;
using Electronics_store_db_wpf.Services.NavigationService;

namespace Electronics_store_db_wpf.MVVM.ViewModel
{
    public class EmployeeViewModel : Core.ViewModel
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _roleRepository;
        private INavigationService _navigation;

        public RelayCommand UpdateDataCommand { get; set; }
        public RelayCommand AddingNewItemToDgEmployeesCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand FilterCommand { get; set; }
        public RelayCommand SavePasswordCommand { get; set; }
        public RelayCommand ChangeImageCommand { get; set; }
        public INavigationService Navigation
        {
            get { return _navigation; }
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        private PasswordInput _passwordInput = new PasswordInput();
        public PasswordInput PasswordInput 
        {
            get { return _passwordInput; }
            set
            {
                _passwordInput = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _roleName;
        public ObservableCollection<string> RoleName
        {
            get { return _roleName; }
            set
            {
                _roleName = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<WrapperObject<Employee>> _employeesCollection;
        public ObservableCollection<WrapperObject<Employee>> EmployeesCollection
        {
            get { return _employeesCollection; }
            set
            {
                _employeesCollection = value;
                OnPropertyChanged();
            }
        }
     

        private WrapperObject<Employee> _selectedEmployee;
        
        public WrapperObject<Employee> SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                if (_selectedEmployee?.DatabaseModel != null && PropertyValidator.Employee(_selectedEmployee.DatabaseModel))
                {
                    _selectedEmployee.ErrorValidation.HasError = false;
                    AddOrUpdate(_selectedEmployee.DatabaseModel);
                }
                else if (_selectedEmployee?.DatabaseModel != null)
                {
                    _selectedEmployee.ErrorValidation.HasError = true;
                }
                _selectedEmployee = value;
                OnPropertyChanged();


            }
        }

        private FilterEmployee _filterOptions;
        public FilterEmployee FilterOptions
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

        public EmployeeViewModel(INavigationService navigation, IEmployeeRepository employeeRepository, IRoleRepository roleRepository)
        {
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
            Navigation = navigation;

            FilterCommand = new RelayCommand(FilterItems, CanFilterItems);
            DeleteCommand = new RelayCommand(DeleteItems, CanDeleteItems);
            SavePasswordCommand = new RelayCommand(SavePassword, CanSavePassword);
            AddingNewItemToDgEmployeesCommand = new RelayCommand(AddingNewItemToDgEmployees, CanAddingNewItemToDgEmployees);

          
            UpdateDataCommand = new RelayCommand(UpdateData, CanUpdateData);
            ChangeImageCommand = new RelayCommand(ChangeImage, CanChangeImage);

            _ = Initiaize();
        }

        private async Task Initiaize()
        {
            FilterOptions = new FilterEmployee();

            EmployeesCollection = new ObservableCollection<WrapperObject<Employee>>(WrapperObject<Employee>.WrapList(await _employeeRepository.GetAllAsync()));
            RoleName = new ObservableCollection<string>((await _roleRepository.GetAllAsync()).Select(r => r.Name));
        }
        private void UpdateData(object obj)
        {
            _ = Initiaize();
        }
        private bool CanUpdateData(object obj) => true;


        private void SavePassword(object obj)
        {
            if (SelectedEmployee == null)
            {
                return;
            }
            if (PasswordInput.NewPassword != PasswordInput.ConfirmPassword)
            {
                return;
            }

            if (string.IsNullOrEmpty(SelectedEmployee.DatabaseModel.Passwordhash))
            {

                SelectedEmployee.DatabaseModel.Passwordhash = BCrypt.Net.BCrypt.HashPassword(PasswordInput.NewPassword);
                
            }
            else if(!string.IsNullOrEmpty(PasswordInput.OldPassword))
            {
                if( BCrypt.Net.BCrypt.Verify(PasswordInput.OldPassword,SelectedEmployee.DatabaseModel.Passwordhash))
                {

                    SelectedEmployee.DatabaseModel.Passwordhash = BCrypt.Net.BCrypt.HashPassword(PasswordInput.NewPassword);
                    SelectedEmployee = SelectedEmployee;
                }
            }
        }

        private bool CanSavePassword(object obj) => true;

        private void ChangeImage(object parameter)
        {
            if (SelectedEmployee.DatabaseModel != null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif) | *.jpg; *.jpeg; *.png; *.gif";

                if (openFileDialog.ShowDialog() == true)
                {
                    string imagePath = openFileDialog.FileName;
                    SelectedEmployee.DatabaseModel.Image = imagePath;
                 
                }
            }
        }
        private bool CanChangeImage(object parameter) => SelectedEmployee?.DatabaseModel != null;

        private async void FilterItems(object parameter)
        {
            if (FilterOptions == null) { return; }

            List<Employee> _employees = await _employeeRepository.GetAllAsync();


            var employeeList = FilterHelper.FilterEmployees(_employees, FilterOptions);

            EmployeesCollection = new ObservableCollection<WrapperObject<Employee>>(WrapperObject<Employee>.WrapList(FilterHelper.FilterEmployees(_employees, FilterOptions)));

        }
        private bool CanFilterItems(object parameter) => true;
       
        private void AddingNewItemToDgEmployees(object parameter)
        {
            if (parameter is AddingNewItemEventArgs args)
            {
                args.NewItem = new WrapperObject<Employee>() { DatabaseModel = new Employee { Role = new Role() } };
            }
        }
        private bool CanAddingNewItemToDgEmployees(object parameter) => true;

        private async void DeleteItems(object parameter)
        {


            if (parameter == null)
            {
                return;
            }

            var removedEmployees = ((IList)parameter)?.OfType<WrapperObject<Employee>>().ToList();

            if (removedEmployees == null || EmployeesCollection == null)
            {
                return;
            }
         

            foreach (var item in removedEmployees)
            {
                var productToRemove = EmployeesCollection?.FirstOrDefault(p => p.DatabaseModel.Id == item.DatabaseModel.Id);
                if (productToRemove != null)
                {
                    EmployeesCollection.Remove(productToRemove);
                }
            }

            EmployeesCollection = EmployeesCollection;

            await _employeeRepository.DeleteByIdsAsync(removedEmployees.Select(p => p.DatabaseModel.Id).ToList());


        }
        private bool CanDeleteItems(object parameter) => parameter != null;


        private async void AddOrUpdate(Employee _selectedEmployee)
        {

            if (_selectedEmployee != null)
            {

                var exists = await _employeeRepository.CheckRecordByIdAsync(_selectedEmployee.Id);

                if (exists)
                {
                    await _employeeRepository.UpdateAsync(_selectedEmployee);
                }
                else
                {
                    await _employeeRepository.AddAsync(_selectedEmployee);
                }
            }

        }

    }
}
    
