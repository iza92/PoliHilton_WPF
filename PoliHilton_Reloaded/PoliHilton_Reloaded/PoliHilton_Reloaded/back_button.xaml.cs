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

namespace PoliHilton_Reloaded
{
    /// <summary>
    /// Interaction logic for back_button.xaml
    /// </summary>
    public partial class back_button : UserControl
    {
        public back_button()
        {
            InitializeComponent();
        }

        private void go_back(object sender, MouseButtonEventArgs e)
        {
            App.Current.Windows[App.Current.Windows.Count - 2].Show();
            App.Current.Windows[App.Current.Windows.Count - 1].Close();
        }
    }
}
