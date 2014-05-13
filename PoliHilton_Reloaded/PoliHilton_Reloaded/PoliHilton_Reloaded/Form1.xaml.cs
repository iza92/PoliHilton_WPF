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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Form1 : Window
    {
        Auth auth1;
        Database db1;
        //Two constructors one is needed if we log out the other for basic log in
        //every button must call a function of the class that controlls it : required by Prof
        public Form1()
        {
            InitializeComponent();
            db1 = new Database();
            db1.init();
            auth1 = new Auth(db1);
        }
        public Form1(Database db1)
        {
            this.db1 = db1;
            InitializeComponent();
            auth1 = new Auth(db1);
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

        private void form1_button_signin_Click(object sender, RoutedEventArgs e)
        {
            int id = auth1.login(form1_textBoxUsername.Text.ToString(), form1_textBoxPass.Password.ToString());
            switch (id)
            {
                case 1: login_user(form1_textBoxUsername.Text.ToString()); this.Hide(); break;
                case 2: login_admin(form1_textBoxUsername.Text.ToString()); this.Hide(); break;
                case 3: login_cleaner(form1_textBoxUsername.Text.ToString()); this.Hide(); break;
                case 4: login_reception(form1_textBoxUsername.Text.ToString()); this.Hide(); break;
            }
        }

        private void form1_button_signup_Click(object sender, RoutedEventArgs e)
        {
            if (form1_tab2_firstName.Text == "" || form1_tab2_pass.Password.ToString() == "" || form1_tab2_lastName.Text == "" || form1_tab2_username.Text == "")
            {
                MessageBox.Show("The field must not be NULL");
            }
            else if (form1_tab2_pass.Password.ToString() != form1_tab2_confpass.Password.ToString())
            {
                MessageBox.Show("passwords doesnt match");
            }
            else
            {
                auth1.create_user(form1_tab2_username.Text, form1_tab2_pass.Password.ToString(), form1_tab2_firstName.Text, form1_tab2_lastName.Text);
            }
        }
        public void login_user(String username)
        {
            Form5 f5 = new Form5(username, this.db1);
        }

        public void login_admin(String username)
        {
         //   Form2 f2 = new Form2(username, this.db1);
        }
        public void login_cleaner(String username)
        {
            Form3 f3 = new Form3(username, this.db1);
        }
        public void login_reception(String username)
        {
            Form4 f4 = new Form4(username, this.db1);
        }
    }
}
