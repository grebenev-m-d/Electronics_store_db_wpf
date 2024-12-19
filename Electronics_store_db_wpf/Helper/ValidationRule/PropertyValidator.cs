using Electronics_store_db_wpf.Data.DatabaseModel;

namespace Electronics_store_db_wpf.Helper.ValidationRule
{
    static class PropertyValidator
    {
        public static bool Order(Order order)
        {
            if (order.Client == null)
            {
                return false;
            }

            if (order.OrderDate == null)
            {
                return false;
            }

            return true;
        }
        public static bool OrderItem(OrderItem orderItem)
        {
            if (orderItem.Order == null)
            {
                return false;
            }
            if (orderItem.Product == null)
            {
                return false;
            }
            if (!orderItem.Quantity.HasValue || orderItem.Quantity <= 0)
            {
                return false;
            }
            if (!orderItem.Amount.HasValue || orderItem.Amount <= 0)
            {
                return false;
            }

            return true;
        }
        public static bool Client(Client? client)
        {
            if (client == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(client.Surname))
            {
                return false;
            }
            if (string.IsNullOrEmpty(client.FirstName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(client.Patronymic))
            {
                return false;
            }
            if (string.IsNullOrEmpty(client.Email) || !Validator.Email(client.Email))
            {
                return false;
            }
            if (string.IsNullOrEmpty(client.Phone) || !Validator.Phone(client.Phone))
            {
                return false;
            }
            if (!client.Birthday.HasValue || !Validator.Birthday(client.Birthday.Value))
            {
                return false;
            }

            return true;
        }
        public static bool Product(Product? product)
        {
            if (product == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(product.Name))
            {
                return false;
            }
            if (string.IsNullOrEmpty(product.Description))
            {
                return false;
            }
            if (!product.Price.HasValue || product.Price <= 0)
            {
                return false;
            }
            if (string.IsNullOrEmpty(product.Image))
            {
                return false;
            }
            if (!product.Quantity.HasValue || product.Quantity <= 0)
            {
                return false;
            }
            if (product.Category == null)
            {
                return false;
            }

            return true;
        }
    

        public static bool Employee(Employee? employee)
        {
            if (employee == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(employee.Surname))
            {
                return false;
            }
            if (string.IsNullOrEmpty(employee.FirstName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(employee.Patronymic))
            {
                return false;
            }
            if (string.IsNullOrEmpty(employee.Position))
            {
                return false;
            }
            if (employee.Salary.HasValue && employee.Salary < 16000)
            {
                return false;
            }
            if (!Validator.Email(employee.Email))
            {
                return false;
            }
            if (!Validator.Phone(employee.Phone))
            {
                return false;
            }
            if (!Validator.Birthday(employee.Birthday))
            {
                return false;
            }
            if (string.IsNullOrEmpty(employee.Passwordhash))
            {
                return false;
            }
            if (string.IsNullOrEmpty(employee.Role.Name))
            {
                return false;
            }

            return true;
        }
    }
}
