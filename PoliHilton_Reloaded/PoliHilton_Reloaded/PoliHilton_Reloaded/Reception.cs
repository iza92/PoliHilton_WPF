using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Controls;
using System.Windows;

namespace PoliHilton_Reloaded
{
   public class Reception
   {
       //We need functions for all the buttons/actions posssible selected
       //Initalization is done and log out
       Database db1;
       int id;
       String firstname;
       String lastname;
       Form4 f4;
       public Reception(int id,String firstname,String lastname,Database db1)
       {
            this.db1=db1;
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
        
       }
       public void reception_dataset_populate_rname(System.Windows.Controls.ComboBox g1)
       {
           String command_cleaner = "SELECT r_number FROM [polihilton].[dbo].[Rooms]";
           DataSet ds1 = db1.Read(command_cleaner);
           //am modificat sa ia fiecare row din tabel si sa le adauge, se poate si cu source cred dar asa e mai simplu
           foreach (DataTable table in ds1.Tables)
           {
               foreach (DataRow dr in table.Rows)
               {
                g1.Items.Add(dr["r_number"].ToString());
               }
           }
       }
       public void reception_dataset_populate_uname(ComboBox g1)
       {
           String command_cleaner = "SELECT username FROM [polihilton].[dbo].[Users]";
           DataSet ds1 = db1.Read(command_cleaner);
           //am modificat sa ia fiecare row din tabel si sa le adauge, se poate si cu source cred dar asa e mai simplu
           foreach (DataTable table in ds1.Tables)
           {
               foreach (DataRow dr in table.Rows)
               {
                   g1.Items.Add(dr["username"].ToString());

               }
           }
       }
       public void create_rezervation(int room_id,int user_id,DateTime s_date,DateTime e_date, int r_price)
       {
           String db_command1 = "INSERT INTO [polihilton].[dbo].[Rezervations] (r_id,u_id,start_date,end_date,rez_price)Values('" + room_id + "','" + user_id + "','" + s_date + "','" + e_date + "','" + r_price + "')";
           db1.Command(db_command1);
           MessageBox.Show("Reservation created succesfully!");
       }
       public void create_user(String username, String password, String firstname, String lastname, int u_type_id)
       {
           String db_command = "SELECT * FROM [polihilton].[dbo].[Users] Where username='" + username + "'";
           DataSet ds1 = db1.Read(db_command);
           if (ds1.Tables[0].Rows.Count != 0)
           {
               MessageBox.Show("User already taken");
           }
           else if(u_type_id==2)
           {
               MessageBox.Show("You cannot create an administrator account");
           }
           else if (username=="" ||lastname=="" ||firstname=="" ||password=="" )
           {
               MessageBox.Show("No field should be left NULL");
           }
           else
           {
               try
               {
                   String db_command1 = "INSERT INTO [polihilton].[dbo].[Users] (u_type_id,username,password,firstName,lastName)Values('" + u_type_id + "','" + username + "','" + password + "','" + firstname + "','" + lastname + "')";
                   db1.Command(db_command1);
                   MessageBox.Show("User created succesfully!");
               }
               catch (Exception e)
               {
                   MessageBox.Show("Error Insert" + e.ToString());
               }
           }
       }
       public int calculate_price(int room_number)
       {
           String command_cleaner = "SELECT * FROM [polihilton].[dbo].[RoomTypes] JOIN [polihilton].[dbo].[Rooms] ON [polihilton].[dbo].[RoomTypes].r_type_id=[polihilton].[dbo].[Rooms].r_type_id WHERE r_number='" + room_number + "' ";
           DataSet ds1 = db1.Read(command_cleaner);
           foreach (DataTable table in ds1.Tables)
           {
               foreach (DataRow dr in table.Rows)
               {
                   room_number=int.Parse(dr["price"].ToString());
               }
           }
           return room_number;
       }
       public int return_rid(int room_number)
       {
           String command_cleaner = "SELECT * FROM [polihilton].[dbo].[Rooms] [Rooms] WHERE r_number='" + room_number + "' ";
           DataSet ds1 = db1.Read(command_cleaner);
           foreach (DataTable table in ds1.Tables)
           {
               foreach (DataRow dr in table.Rows)
               {
                   room_number = int.Parse(dr["r_id"].ToString());
               }
           }
           return room_number;
       }
       public int return_uid(string u_name)
       {
           String command_cleaner = "SELECT * FROM [polihilton].[dbo].[Users] [Rooms] WHERE username='" + u_name + "' ";
           DataSet ds1 = db1.Read(command_cleaner);
           foreach (DataTable table in ds1.Tables)
           {
               foreach (DataRow dr in table.Rows)
               {
                   u_name=dr["u_id"].ToString();
               }
           }
           return int.Parse(u_name);
       }
       public void reception_dataset_populate(DataGrid g1)
       {
           String command_reception = "SELECT * FROM [polihilton].[dbo].[Rezervations]";
           DataSet ds1 = db1.Read(command_reception);
           foreach (DataTable table in ds1.Tables)
           {
               g1.ItemsSource = table.DefaultView;
           }
       }
      
       public void delete_rezervation(DataGrid g1)
       {/*
           int rowindex = g1.Items.IndexOf(g1.CurrentItem);
           string value=g1.Rows[rowindex].Cells[0].Value.ToString();
           String db_command = "DELETE FROM [polihilton].[dbo].[Rezervations] Where rez_id='" +int.Parse(value)  + "' ";
           DataSet ds1 = db1.Read(db_command);
           MessageBox.Show("Reservation deleted!"); */
       }
       public void log_out()
       {
           this.f4.Close();
           Form1 f1 = new Form1(this.db1);
       }

    }
}
