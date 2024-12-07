using Electronics_store_db_wpf.Core;

namespace Electronics_store_db_wpf.Services.NavigationService
{
    public interface INavigationService
    {
        ViewModel CurrentMainWindowView { get; }
        ViewModel CurrentMainContentView { get; }

        void NavigateToMainWindow<T>() where T : ViewModel;
        void NavigateToMainContent<T>() where T : ViewModel;
    }
}
