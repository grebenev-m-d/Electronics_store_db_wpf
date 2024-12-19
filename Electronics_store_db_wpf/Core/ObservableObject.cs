using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Electronics_store_db_wpf.Core
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? proprtyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proprtyName));
        }
    }
}
