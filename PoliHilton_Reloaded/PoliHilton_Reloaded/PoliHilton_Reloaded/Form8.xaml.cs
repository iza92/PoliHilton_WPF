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

namespace PoliHilton_Reloaded
{
    /// <summary>
    /// Interaction logic for Form8.xaml
    /// </summary>
    public partial class Form8 : Window
    {
         Users u1;
        int room_number;
        DateTime startDate;
        DateTime endDate;
        public Form8(Users u1,int room_number,DateTime s,DateTime e)
        {
            InitializeComponent();
            this.u1 = u1;
            this.room_number = room_number;
            this.Show();
          
            form8_dtp_start.SelectedDate= s.Date;
            form8_dtp_end.SelectedDate= e.Date;
            u1.fill_room_fields_final(form8_tb_roomName, form8_tb_roomNo, form8_tb_roomFloor, form8_tb_roomCap, form8_tb_roomSurface,form8_tb_roomOrientation,form8_tb_roomPrice,form8_tb_roomDisc,form8_tb_total,room_number);
        }

        
       
        private void form8_btn_reserve_Click(object sender, EventArgs e)
        { /*
            int result=u1.final_reserver_form8(form8_tb_roomNo, textBox1, textBox2,form8_tb_total);
            if (result == 1)
            {
                MessageBox.Show("Successfully Reserved");
            }
            else
            {
                MessageBox.Show("There is already a reservation for that room for that date");
            }
            this.Close(); */
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

        private void form8_btn_Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
