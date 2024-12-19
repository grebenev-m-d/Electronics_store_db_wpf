using System;

namespace Electronics_store_db_wpf.MVVM.Model
{
    public class FilterEmployee
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Position { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Role { get; set; }
    }
}
