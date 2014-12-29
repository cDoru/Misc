using System.Windows.Controls;
using Microsoft.Practices.Prism.Mvvm;

namespace HelloWorldModule
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : IView
    {
        public MainView(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }
    }
}