using System;

#nullable disable

namespace Electronics_store_db_wpf.Data.DatabaseModel
{
    public partial class Employee : BaseEntity
    {
        private string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _patronymic;
        public string Patronymic
        {
            get { return _patronymic; }
            set
            {
                if (_patronymic != value)
                {
                    _patronymic = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _position;
        public string Position
        {
            get { return _position; }
            set
            {
                if (_position != value)
                {
                    _position = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _image;
        public string Image
        {
            get { return _image; }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal? _salary;
        public decimal? Salary
        {
            get { return _salary; }
            set
            {
                if (_salary != value)
                {
                    _salary = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime? _birthday;
        public DateTime? Birthday
        {
            get { return _birthday; }
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _passwordhash;
        public string Passwordhash
        {
            get { return _passwordhash; }
            set
            {
                if (_passwordhash != value)
                {
                    _passwordhash = value;
                    OnPropertyChanged();
                }
            }
        }

        private Guid? _roleId;
        public Guid? RoleId
        {
            get { return _roleId; }
            set
            {
                if (_roleId != value)
                {
                    _roleId = value;
                    OnPropertyChanged();
                }
            }
        }

        private Role _role;
        public virtual Role Role
        {
            get { return _role; }
            set
            {
                if (_role != value)
                {
                    _role = value;
                    OnPropertyChanged();
                }
            }
        }


        public void UpdateProperties(Employee updatedEmployee)
        {
            // Проверяем, есть ли обновления для каждого поля и обновляем их, если есть
            if (updatedEmployee.Surname != null)
            {
                this.Surname = updatedEmployee.Surname;
            }
            if (updatedEmployee.FirstName != null)
            {
                this.FirstName = updatedEmployee.FirstName;
            }
            if (updatedEmployee.Patronymic != null)
            {
                this.Patronymic = updatedEmployee.Patronymic;
            }
            if (updatedEmployee.Position != null)
            {
                this.Position = updatedEmployee.Position;
            }
            if (updatedEmployee.Image != null)
            {
                this.Image = updatedEmployee.Image;
            }
            if (updatedEmployee.Salary != null)
            {
                this.Salary = updatedEmployee.Salary;
            }
            if (updatedEmployee.Email != null)
            {
                this.Email = updatedEmployee.Email;
            }
            if (updatedEmployee.Phone != null)
            {
                this.Phone = updatedEmployee.Phone;
            }
            if (updatedEmployee.Birthday.HasValue)
            {
                this.Birthday = updatedEmployee.Birthday;
            }
            if (updatedEmployee.Passwordhash != null)
            {
                this.Passwordhash = updatedEmployee.Passwordhash;
            }
            if (updatedEmployee.RoleId != null)
            {
                this.RoleId = updatedEmployee.RoleId;
            }
        }
    }
}
