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
using System.Windows.Shapes;
using System.Data;

namespace PoliHilton_Reloaded
{
    /// <summary>
    /// Interaction logic for Form5.xaml
    /// </summary>
    public partial class Form5 : Window
    {
        Users u1;
        Database db1;
        public Form5(String username, Database db1)
        {
            InitializeComponent();
            this.Show();
            this.db1 = db1;
            init_user(username);
            u1.list_current_reservations(Form5_lb);
        }

        public void init_user(String username)
        {
            String db_command = "SELECT * FROM [polihilton].[dbo].[Users] Where username='" + username + "'";
            DataSet ds1 = db1.Read(db_command);
            DataRow dr1 = ds1.Tables[0].Rows[0];
            this.u1 = new Users(int.Parse(dr1["u_id"].ToString()), dr1["firstName"].ToString(), dr1["lastName"].ToString(), dr1["username"].ToString(), this.db1, Form5_label_name);
        }

        private void DragForm(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseForm(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void form5_btn_newRes_Click(object sender, EventArgs e)
        {
            Form7 f7 = new Form7(u1);
            this.Hide();

        }

        private void form5_btn_getRes_Click(object sender, EventArgs e)
        {
            u1.list_current_reservations(Form5_lb);
        }

        private void form5_btn_cancelRes_Click(object sender, EventArgs e)
        {
            u1.cancel_reservation(Form5_lb);
            u1.list_current_reservations(Form5_lb);
        }
    }
}
