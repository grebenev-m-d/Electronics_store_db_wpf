using Electronics_store_db_wpf.Data.DatabaseModel;

namespace Electronics_store_db_wpf.Data.Repository.CurrentEmployeeRepository
{
    public class CurrentEmployeeRepository : ICurrentEmployeeRepository
    {
        private Employee _currentEmployee;
        public Employee CurrentEmployee
        {
            get { return _currentEmployee; }
            set
            {
                if (_currentEmployee != value)
                {
                    _currentEmployee = value;
                    //OnPropertyChanged(nameof(UserName));
                }
            }
        }
    }
}
