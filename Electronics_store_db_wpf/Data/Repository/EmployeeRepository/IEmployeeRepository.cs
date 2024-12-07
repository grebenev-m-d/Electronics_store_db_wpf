using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.EFcommonRepository;
using System.Threading.Tasks;

namespace Electronics_store_db_wpf.Data.Repository.EmployeeRepository
{
    public interface IEmployeeRepository : IEFcommonRepository<Employee>
    {
        public Task<Employee> GetByFullNameAsync(string surname, string firstName, string patronymic);
    }
}
