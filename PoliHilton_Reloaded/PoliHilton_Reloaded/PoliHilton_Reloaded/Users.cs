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


    }
}
