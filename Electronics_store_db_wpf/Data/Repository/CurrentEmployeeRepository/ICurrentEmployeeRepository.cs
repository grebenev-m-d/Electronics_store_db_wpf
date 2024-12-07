using Electronics_store_db_wpf.Data.DatabaseModel;

namespace Electronics_store_db_wpf.Data.Repository.CurrentEmployeeRepository
{
    public interface ICurrentEmployeeRepository
    {
        public Employee CurrentEmployee { get; set; }
    }
}
