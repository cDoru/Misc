using System;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;

namespace PrismTesting
{
    public class Bootstrapper : UnityBootstrapper
    {
        protected override IUnityContainer CreateContainer()
        {
            IUnityContainer container = base.CreateContainer();


            return container;
        }

        protected override DependencyObject CreateShell()
        {
            var shell = new Shell();
            shell.Show();
            return shell;
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;

            moduleCatalog.AddModule(typeof(HelloWorldModule.Module));
        }
    }
}