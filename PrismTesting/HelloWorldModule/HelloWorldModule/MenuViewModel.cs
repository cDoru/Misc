using System;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;

namespace HelloWorldModule
{
    public class MenuViewModel : INavigationAware
    {
        private IRegionManager _regionManager;
        public ICommand MainCommand { get; set; }
        public ICommand SecondCommand { get; set; }

        public MenuViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            MainCommand = new DelegateCommand(MainCommandAction);
            SecondCommand = new DelegateCommand(SecondCommandAction);
        }

        private void MainCommandAction()
        {
            _regionManager.RequestNavigate("Main", new Uri("MainView", UriKind.Relative));
        }

        private void SecondCommandAction()
        {
            _regionManager.RequestNavigate("Main", new Uri("SecondView", UriKind.Relative));
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
