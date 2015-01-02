using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualStudio.TextTemplating;

namespace WpfApplication4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var session = new TextTemplatingSession();

            var template1 = new Template1();

            session["TestParam"] = Parameter.Text;

            session["List"] = new System.Collections.Generic.List<string>
            {
                "One",
                "Two"
            };

            template1.Session = session;
            template1.Initialize();

            Output.Text = template1.TransformText();
        }
    }
}
