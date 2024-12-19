using Electronics_store_db_wpf.Data.DatabaseModel;
using Electronics_store_db_wpf.MVVM.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Electronics_store_db_wpf.Core
{
    
    public class WrapperObject<T> where T : BaseEntity
    {
        public WrapperObject()
        {
            ErrorValidation = new ErrorValidation();
        }

        public T DatabaseModel { get; set; }
        public ErrorValidation ErrorValidation { get; set; }

        public static ObservableCollection<WrapperObject<T>> WrapList(List<T> list) 
        {
            var pairList = new ObservableCollection<WrapperObject<T>>();

            foreach (var item in list)
            {
                var pair = new WrapperObject<T>
                {
                    DatabaseModel = item
                };

                pairList.Add(pair);
            }

            return pairList;
        }
    }
}
