using System;
using System.Text.RegularExpressions;

namespace Electronics_store_db_wpf.Helper.ValidationRule
{
    static class Validator
    {
        public static bool Email(string email)
        {
            if (email == null)
                return false;

            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match match = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            return match.Success;

        }
        public static bool Phone(string phone)
        {
            if (phone == null) 
                return false;

            string pattern = "^(?:\\+7|8)\\d{10}$";
            Match match = Regex.Match(phone, pattern, RegexOptions.IgnoreCase);
            return match.Success;
        }
      
        public static bool Birthday(DateTime? birthday)
        {
            if (!birthday.HasValue) 
                return false; 

            DateTime minDate = new DateTime(1900, 1, 1);
            DateTime maxDate = DateTime.Now.AddYears(-14);



            if (birthday >= minDate && birthday <= maxDate)
            {
                return true;
            }

            return false;

        }

    }
}
