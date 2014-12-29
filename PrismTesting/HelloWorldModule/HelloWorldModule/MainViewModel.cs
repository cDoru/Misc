using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace HelloWorldModule
{
    public class MainViewModel : INavigationAware
    {
        private IRegionManager _regionManager;
        public string Title { get; set; }

        public ICommand MyCommand { get; set; }

        public MainViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Title = "Main";
            MyCommand = new DelegateCommand(MyCommandAction);

        }

        private void MyCommandAction()
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
