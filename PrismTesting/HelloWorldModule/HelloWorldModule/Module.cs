using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace HelloWorldModule
{
    public class Module : IModule
    {
        private readonly IRegionManager _regionManager;

        public Module(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;

            container.RegisterType<MainViewModel>();
            container.RegisterType<MenuViewModel>();
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("Menu", typeof(MenuView));
            _regionManager.RegisterViewWithRegion("Main", typeof (MainView));
            _regionManager.RegisterViewWithRegion("Main", typeof(SecondView));
        }
    }
}
