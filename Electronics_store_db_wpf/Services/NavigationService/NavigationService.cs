using Electronics_store_db_wpf.Core;
using System;

namespace Electronics_store_db_wpf.Services.NavigationService
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private readonly Func<Type, ViewModel> _viewModelFactory;
        private ViewModel _currentMainWindowView;
        private ViewModel _currentMainContentView;

        // Свойство для текущего представления главного окна
        public ViewModel CurrentMainWindowView
        {
            get => _currentMainWindowView;
            private set
            {
                _currentMainWindowView = value;
                OnPropertyChanged();
            }
        }

        // Свойство для текущего представления основного содержимого
        public ViewModel CurrentMainContentView
        {
            get => _currentMainContentView;
            private set
            {
                _currentMainContentView = value;
                OnPropertyChanged();
            }
        }

        public NavigationService(Func<Type, ViewModel> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        // Метод для перехода к представлению главного окна
        public void NavigateToMainWindow<TViewModel>() where TViewModel : ViewModel
        {
            // Создание экземпляра указанной модели представления с использованием фабрики моделей представлений
            ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));

            // Установка текущего представления главного окна в созданную модель представления
            CurrentMainWindowView = viewModel;
        }

        // Метод для перехода к представлению основного содержимого
        public void NavigateToMainContent<TViewModel>() where TViewModel : ViewModel
        {
            // Создание экземпляра указанной модели представления с использованием фабрики моделей представлений
            ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));

            // Установка текущего представления основного содержимого в созданную модель представления
            CurrentMainContentView = viewModel;
        }
    }
}
