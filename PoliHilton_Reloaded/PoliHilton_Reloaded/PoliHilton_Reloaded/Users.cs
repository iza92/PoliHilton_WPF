using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;

namespace PoliHilton_Reloaded
{
    public class Users
    {
     Database db1;
     int id;
     String firstname;
     String lastname;
     public String username;
     Form5 f5;
     Form6 f6;
     Form7 f7;
     Form8 f8;

     public Users(int id, String firstname, String lastname, String username, Database db1, System.Windows.Controls.Label l1)
     {

         this.db1 = db1;
         this.id = id;
         this.firstname = firstname;
         this.lastname = lastname;
         this.username = username;
         l1.Content = username;
     }

       public DataSet list_current_reservations(System.Windows.Controls.ListBox l1)
       {
           l1.Items.Clear();
           DataSet ds_reservations = new DataSet();
           String command_cleaner = "SELECT * FROM [polihilton].[dbo].[Rezervations] WHERE u_id='" + id + "'";
           DataSet ds1 = db1.Read(command_cleaner);
           foreach (DataRow dr in ds1.Tables[0].Rows)
           {
               char[] separator = { ' ' };
               string[] words1 = dr.ItemArray.GetValue(3).ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
               string[] words2 = dr.ItemArray.GetValue(4).ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
               String line = "room: " + dr.ItemArray.GetValue(2).ToString() + "  start date: " + words1[0]
                   + "  end date: " + words2[0] + "  price: " + dr.ItemArray.GetValue(5).ToString();
               l1.Items.Add(line);
           }
           return ds_reservations;
       }


       public void cancel_reservation(System.Windows.Controls.ListBox l1)
       {
           char[] separator = { ' ' };
           try
           {
               string[] words = l1.SelectedItem.ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
               String command = "DELETE FROM [polihilton].[dbo].[Rezervations] WHERE r_id = '" + int.Parse(words[1]) + "'";
               db1.Command(command);
           }
           catch (Exception e)
           {
               Console.WriteLine("{0} Exception caught.", e);
           }
       }


       public void log_out()
       {
           Form1 f1 = new Form1(this.db1);
           Application.Current.Windows[0].Hide();
           f1.Show();
       }

       public void new_reservation()
       {

       }

       public void fill_room_fields_final(System.Windows.Controls.TextBox t1, System.Windows.Controls.TextBox t2, System.Windows.Controls.TextBox t3, System.Windows.Controls.TextBox t4,
          System.Windows.Controls.TextBox t5, System.Windows.Controls.TextBox t6, System.Windows.Controls.TextBox t7, System.Windows.Controls.TextBox t8, System.Windows.Controls.TextBox t9, int room_number)
       {
           String command = "SELECT t1.name, t2.r_number, t2.r_floor, t1.capacity, t2.surface, t2.orientation, t1.price FROM [polihilton].[dbo].[RoomTypes] t1 JOIN [polihilton].[dbo].[Rooms] t2 ON t1.r_type_id=t2.r_type_id WHERE t2.r_id='" + room_number + "'";
           DataSet ds1 = db1.Read(command);
           foreach (DataRow dr in ds1.Tables[0].Rows)
           {
               command = "SELECT * FROM [polihilton].[dbo].[Discounts] WHERE r_id='" + room_number + "'";
               DataSet ds2 = db1.Read(command);
               if (ds2.Tables[0].Rows.Count == 0)
               {
                   t8.Text = "0";
                   t9.Text = dr.ItemArray.GetValue(6).ToString();
               }
               else
               {
                   DataRow dr1 = ds2.Tables[0].Rows[0];
                   int new_price = int.Parse(dr1["price"].ToString());
                   t8.Text = "" + (int.Parse(dr["price"].ToString()) - new_price);
                   t9.Text = new_price.ToString();
               }
               t1.Text = dr.ItemArray.GetValue(0).ToString();
               t2.Text = dr.ItemArray.GetValue(1).ToString();
               t3.Text = dr.ItemArray.GetValue(2).ToString();
               t4.Text = dr.ItemArray.GetValue(3).ToString();
               t5.Text = dr.ItemArray.GetValue(4).ToString();
               t6.Text = dr.ItemArray.GetValue(5).ToString();
               t7.Text = dr.ItemArray.GetValue(6).ToString();
           }
       }


    }
}
