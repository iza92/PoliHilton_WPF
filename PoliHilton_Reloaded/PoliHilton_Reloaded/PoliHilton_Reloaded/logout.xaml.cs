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
    /// Interaction logic for logout.xaml
    /// </summary>
    public partial class logout : UserControl
    {
        public logout()
        {
            InitializeComponent();
        }

        private void log_out(object sender, MouseButtonEventArgs e)
        {

            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 1; intCounter--)
                App.Current.Windows[intCounter].Close();
            App.Current.Windows[0].Show();
        }
    }
}
