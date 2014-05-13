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
    /// Interaction logic for Form4.xaml
    /// </summary>
    public partial class Form4 : Window
    {
        Reception r1;
        Database db1;
        public Form4(String username, Database db1)
        {
            InitializeComponent();
            this.db1 = db1;
            this.Show();
            init_reception(username);
            r1.reception_dataset_populate_rname(form4_cb_roomnumber);
            r1.reception_dataset_populate_uname(form4_cb_username);

        }

        public void init_reception(String username)
        {
            String db_command = "SELECT * FROM [polihilton].[dbo].[Users] Where username='" + username + "'";
            DataSet ds1 = db1.Read(db_command);
            DataRow dr1 = ds1.Tables[0].Rows[0];
            this.r1 = new Reception(int.Parse(dr1["u_id"].ToString()), dr1["firstName"].ToString(), dr1["lastName"].ToString(), this.db1);
        }

        private void DragForm(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseForm(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }

        private void form4_button_createrezervation_Click(object sender, EventArgs e)
        {
            if (form4_cb_roomnumber.SelectedIndex >= 0 && form4_cb_username.SelectedIndex >= 0)
            {
                form4_textPrice.Text = r1.calculate_price(int.Parse(form4_cb_roomnumber.SelectedItem.ToString())).ToString();
                DateTime check_in = form4_dtp_checkin.SelectedDate.Value;
                DateTime check_out = form4_dtp_checkout.SelectedDate.Value;
                int uid = r1.return_uid(form4_cb_username.SelectedItem.ToString());
                int rid = r1.return_rid(int.Parse(form4_cb_roomnumber.SelectedItem.ToString()));
                r1.create_rezervation(rid, uid, check_in, check_out, int.Parse(form4_textPrice.Text));

            }
            else
            {
                MessageBox.Show("You must select the room and the username");
            }
        }

        private void form4_button_createuser_Click(object sender, EventArgs e)
        {
            //in case u dont input a usertype it will be by default =1
            if (form4_text_usertypeid.Text == "")
            {
                form4_text_usertypeid.Text = "1";
            }
            r1.create_user(form4_text_username.Text, form4_text_password.Text, form4_text_firstname.Text, form4_text_lastname.Text, int.Parse(form4_text_usertypeid.Text));
        }

        private void form4_button_showrezervations_Click(object sender, EventArgs e)
        {
            r1.reception_dataset_populate(form4_dataviewgrid);
        }

        private void form4_button_deleterezervation_Click(object sender, EventArgs e)
        {
            r1.delete_rezervation(form4_dataviewgrid);
        }
    }
}
