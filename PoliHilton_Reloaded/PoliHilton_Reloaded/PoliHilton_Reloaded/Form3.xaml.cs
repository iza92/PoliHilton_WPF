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
    /// Interaction logic for Form3.xaml
    /// </summary>
    public partial class Form3 : Window
    {
        Cleaning clean1;
        Database db1;
        //every button must call a function of the class that controlls it : required by Prof
        //Final form only log out required
        public Form3(String username, Database db1)
        {
            InitializeComponent();
            this.db1 = db1;
            this.Show();
            init_cleaner(username);
            clean1.list_assigned_rooms(form3_lb);

        }

        public void init_cleaner(String username)
        {
            String db_command = "SELECT * FROM [polihilton].[dbo].[Users] Where username='" + username + "'";
            DataSet ds1 = db1.Read(db_command);
            DataRow dr1 = ds1.Tables[0].Rows[0];
            this.clean1 = new Cleaning(int.Parse(dr1["u_id"].ToString()), dr1["firstName"].ToString(), dr1["lastName"].ToString(), dr1["username"].ToString(), this.db1, Form3_label_name);

        }

        private void DragForm(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseForm(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void form3_btn_inProgress_Click(object sender, EventArgs e)
        {
            clean1.in_progress(form3_lb);
            clean1.list_assigned_rooms(form3_lb);
        }


        private void form3_btn_cleaned_Click(object sender, EventArgs e)
        {
            clean1.cleaned(form3_lb);
            clean1.list_assigned_rooms(form3_lb);
            
        }

        private void form3_btn_asigne_Click(object sender, EventArgs e)
        {
            clean1.list_assigned_rooms(form3_lb);
        }

    }
}
