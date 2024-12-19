using Electronics_store_db_wpf.Core;
using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.CurrentEmployeeRepository;
using Electronics_store_db_wpf.Data.Repository.EmployeeRepository;
using Electronics_store_db_wpf.Services.NavigationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Electronics_store_db_wpf.MVVM.ViewModel
{
    public class LoginViewModel : Core.ViewModel
    {
      
        private readonly IEmployeeRepository _employeeRepository;
        private string _username;
        private string _password;

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
        private string _errorMessage;
      
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {

                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                ShowMessage();
                _errorMessage = "";
            }
        }
        private async void ShowMessage()
        {
            MessageVisibility = Visibility.Visible;
            await Task.Delay(TimeSpan.FromSeconds(5));
            MessageVisibility = Visibility.Hidden;
        }
        private Visibility _messageVisibility = Visibility.Hidden;
        public Visibility MessageVisibility
        {
            get { return _messageVisibility; }
            set
            {
                if (_messageVisibility != value)
                {
                    _messageVisibility = value;
                    OnPropertyChanged(nameof(MessageVisibility));
                }
            }
        }

        private  ICurrentEmployeeRepository _currentEmployee;
        public Employee CurrentEmployee
        {
            get
            {
                return _currentEmployee.CurrentEmployee;
            }
            set
            {
                _currentEmployee.CurrentEmployee = value;
                OnPropertyChanged();
            }
        }
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {

                _password = value;
                OnPropertyChanged(nameof(Password));

            }
        }
        private List<string> _hintList;
        public List<string> HintList
        {
            get { return _hintList; }
            set 
            { 
                _hintList = value; 
                OnPropertyChanged(); 
            }
        }
        public RelayCommand LoginCommand { get; set; }

        public LoginViewModel(INavigationService navigation, IEmployeeRepository employeeRepository ,ICurrentEmployeeRepository currentEmployee)
        {
            Navigation = navigation;
            _currentEmployee = currentEmployee;
            _employeeRepository = employeeRepository;
            _ = Initiaize();


            LoginCommand = new RelayCommand(Login, CanLogin);
        }

        public async Task Initiaize()
        {
            var allEmployee = await _employeeRepository.GetAllAsync();
            HintList = allEmployee.Select(e => $"{e.Surname} {e.FirstName} {e.Patronymic}").ToList();
        }
        private bool CanLogin(object parameter)
        {
            return true;
            //return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private async void Login(object parameter)
        {   
            string[] names = Username?.Trim()?.Split(' ');

            if (names == null || names.Length != 3)
            {
                ErrorMessage = "Некорректное ФИО! Пожалуйста, убедитесь, что вводите полное ФИО, состоящее из трех слов.";
                return;
            }

            var employee = await _employeeRepository.GetByFullNameAsync(names[0], names[1], names[2]);

            if (employee == null)
            {
                ErrorMessage = "Сотрудник с таким ФИО не найден. Пожалуйста, проверьте правильность введенных данных и попробуйте еще раз.";
                return;
            }


            if (Password != null && BCrypt.Net.BCrypt.Verify(Password, employee.Passwordhash))
            {
                _currentEmployee.CurrentEmployee = employee;
                Navigation.NavigateToMainWindow<MainContentViewModel>();
            }
            else
            {
                ErrorMessage = "Неправильный пароль. Пожалуйста, убедитесь, что вводите правильный пароль и попробуйте еще раз.";
            }
        }
    }
}
