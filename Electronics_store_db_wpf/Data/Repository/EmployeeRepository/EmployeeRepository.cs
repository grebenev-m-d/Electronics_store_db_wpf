using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.Data.Repository.EFcommonRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electronics_store_db_wpf.Data.Repository.EmployeeRepository
{
    public class EmployeeRepository : EFcommonRepository<Employee>, IEmployeeRepository
    {
        private readonly IServiceProvider serviceProvider;

        public EmployeeRepository(IServiceProvider _serviceProvider) : base(_serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }

        public override async Task<List<Employee>> GetAllAsync()
        {

            try
            {
                using (var dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {
                    
                    List<Employee> entityList = await dbContext.Employees.Include(u => u.Role).ToListAsync();

                    return entityList;
                }
            }
            catch (Exception e)
            {

                return null;
            }
        }
        public override async Task<bool> AddAsync(Employee employee)
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {

                    var role = await dbContext.Roles.FirstOrDefaultAsync(u => u.Name == employee.Role.Name);


                    if (role?.Id == null)
                    {
                        return false;
                    }
                  

                    employee.Role = role;

                    dbContext.Entry(employee.Role).State = EntityState.Unchanged;
                    dbContext.Employees.Add(employee);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public override async Task<bool> UpdateAsync(Employee employee)
        {
            try
            {
                using (var dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {

                    if (employee == null)
                    {
                        return false;
                    }

                    var existingEmployee = await dbContext.Set<Employee>().FirstOrDefaultAsync(r => r.Id == employee.Id);

                    if (existingEmployee == null)
                    {
                        return false;
                    }

                    if (employee.Role == null)
                    {
                        return false;
                    }
                    var role = await dbContext.Set<Role>().FirstOrDefaultAsync(r => r.Name == employee.Role.Name);

                    if (role == null)
                    {
                        return false;
                    }
                    employee.RoleId = role.Id;

                    existingEmployee.UpdateProperties(employee);

                    dbContext.Set<Employee>().Update(existingEmployee);
                    dbContext.SaveChanges();

                    return true;
                }
            }
            catch (Exception e)
            {
                return false; // Exception occurred, return false
            }
        }
        public async Task<Employee> GetByFullNameAsync(string surname, string firstName, string patronymic)
        {

            if (string.IsNullOrEmpty(surname) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(patronymic))
            {
                return null;
            }
            
            try
            {
                using (var dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<ElektonikaDbContext>())
                {

                    Employee employee = await dbContext.Employees.Include(u => u.Role)
                    .FirstOrDefaultAsync(x => x.Surname.Trim() == surname
                    && x.FirstName.Trim() == firstName
                    && x.Patronymic.Trim() == patronymic);

                    return employee;
                }
            }
            catch (Exception e)
            {

                return null;
            }
        }
    }
}
