using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.MVVM.Model;
using System.Collections.Generic;
using System.Linq;

namespace Electronics_store_db_wpf.Helper
{
    static class FilterHelper
    {
        public static List<Product> FilterProducts(List<Product> products, FilterProduct filter)
        {

            // Фильтрация по названию
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                products = products.Where(p => p.Name != null && p.Name.Contains(filter.Name)).ToList();
            }

            // Фильтрация по промежутку цены
            if (filter.MinPrice > 0 && filter.MaxPrice > 0)
            {
                products = products.Where(p => p.Price >= filter.MinPrice && p.Price <= filter.MaxPrice).ToList();
            }

            // Фильтрация по описанию 
            if (!string.IsNullOrWhiteSpace(filter.Description))
            {
                products = products.Where(p => p.Description != null && p.Description.Contains(filter.Description)).ToList();
            }

            return products;


            
        }
        public static List<Client> FilterClients(List<Client> clients, FilterClients filter)
        {

            // Фильтрация по фамилии
            if (!string.IsNullOrWhiteSpace(filter.Surname))
            {
                clients = clients.Where(c => c.Surname != null && c.Surname.Contains(filter.Surname)).ToList();
            }

            // Фильтрация по имени
            if (!string.IsNullOrWhiteSpace(filter.FirstName))
            {
                clients = clients.Where(c => c.FirstName != null && c.FirstName.Contains(filter.FirstName)).ToList();
            }

            // Фильтрация по отчеству
            if (!string.IsNullOrWhiteSpace(filter.Patronymic))
            {
                clients = clients.Where(c => c.Patronymic != null && c.Patronymic.Contains(filter.Patronymic)).ToList();
            }

            // Фильтрация по электронной почте
            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                clients = clients.Where(c => c.Email != null && c.Email.Contains(filter.Email)).ToList();
            }

            // Фильтрация по номеру телефона
            if (!string.IsNullOrWhiteSpace(filter.Phone))
            {
                clients = clients.Where(c => c.Phone != null && c.Phone.Contains(filter.Phone)).ToList();
            }

            // Фильтрация по дате рождения
            if (filter.StartDate.HasValue && filter.EndDate.HasValue)
            {
                clients = clients.Where(c => c.Birthday >= filter.StartDate && c.Birthday <= filter.EndDate).ToList();
            }

            return clients;

        }

        public static List<Employee> FilterEmployees(List<Employee> employees, FilterEmployee filter)
        {
           
            // Фильтрация по имени
            if (!string.IsNullOrWhiteSpace(filter.Surname))
            {
                employees = employees.Where(e => e.Surname != null && e.Surname.Contains(filter.Surname)).ToList();
            }

            // Фильтрация по имени
            if (!string.IsNullOrWhiteSpace(filter.FirstName))
            {
                employees = employees.Where(e => e.FirstName != null && e.FirstName.Contains(filter.FirstName)).ToList();
            }

            // Фильтрация по отчеству
            if (!string.IsNullOrWhiteSpace(filter.Patronymic))
            {
                employees = employees.Where(e => e.Patronymic != null && e.Patronymic.Contains(filter.Patronymic)).ToList();
            }

            // Фильтрация по должности
            if (!string.IsNullOrWhiteSpace(filter.Position))
            {
                employees = employees.Where(e => e.Position != null && e.Position.Contains(filter.Position)).ToList();
            }

            // Фильтрация по зарплате
            employees = employees.Where(e => e.Salary.HasValue && e.Salary.Value >= filter.MinSalary && e.Salary.Value <= filter.MaxSalary).ToList();

            // Фильтрация по email
            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                employees = employees.Where(e => e.Email != null && e.Email.Contains(filter.Email)).ToList();
            }

            // Фильтрация по телефону
            if (!string.IsNullOrWhiteSpace(filter.Phone))
            {
                employees = employees.Where(e => e.Phone != null && e.Phone.Contains(filter.Phone)).ToList();
            }

            // Фильтрация по дате рождения
            if (filter.StartDate.HasValue && filter.EndDate.HasValue)
            {
                employees = employees.Where(e => e.Birthday >= filter.StartDate && e.Birthday <= filter.EndDate).ToList();
            }

            // Фильтрация по роли
            if (!string.IsNullOrWhiteSpace(filter.Role))
            {
                employees = employees.Where(e => e.Role != null && e.Role.Name.Contains(filter.Role)).ToList();
            }

            return employees;
        }

        public static List<Order> FilterOrders(List<Order> orders, FilterOrders filter)
        {
            List<Order> filteredOrders = orders;


            filteredOrders = filteredOrders.Where(o => o.TotalAmount >= filter.MinAmount && o.TotalAmount <= filter.MaxAmount).ToList();


            if (filter.StartDate.HasValue && filter.EndDate.HasValue)
            {
                filteredOrders = filteredOrders.Where(o => o.OrderDate >= filter.StartDate && o.OrderDate <= filter.EndDate).ToList();
            }

            return filteredOrders;
        }

        public static List<OrderItem> FilterOrderItems(List<OrderItem> orderItems, FilterOrderItems filter)
        {


            if (!string.IsNullOrWhiteSpace(filter.ProductName))
            {
                orderItems = orderItems.Where(oi => oi.Product.Name != null && oi.Product.Name.Contains(filter.ProductName)).ToList();
            }



            orderItems = orderItems.Where(order => order.Quantity >= filter.MinQuantity && order.Quantity <= filter.MaxQuantity).ToList();


            return orderItems;
        }
    }
}
