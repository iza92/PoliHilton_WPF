using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PoliHilton_Reloaded
{
    //We need functions for all the buttons/actions posssible selected
    //Initalization is done and log out

    public class Cleaning
    {
        Database db1;
        int id;
        String firstname;
        String lastname;
        String username;

        public Cleaning(int id, String firstname, String lastname, String username, Database db1, System.Windows.Controls.Label l1)
        {
            this.db1 = db1;
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            l1.Content = username;
        }


        public DataSet list_assigned_rooms(System.Windows.Controls.ListBox l1)
        {
            l1.Items.Clear();
            DataSet ds_rooms = new DataSet();
            String command_cleaner = "SELECT * FROM [polihilton].[dbo].[Cleaning] WHERE u_id='" + id + "' AND status NOT LIKE 'Cleaned'";
            DataSet ds1 = db1.Read(command_cleaner);
            foreach (DataRow dr in ds1.Tables[0].Rows)
            {
                String line = "room id: " + dr.ItemArray.GetValue(1).ToString() + "  status: " + dr.ItemArray.GetValue(3).ToString();
                l1.Items.Add(line);
            }
            return ds_rooms;


        }
        public void in_progress(System.Windows.Controls.ListBox l1)
        {
            char[] separator = { ' ' };
            try
            {
                string[] words = l1.SelectedItem.ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                String command = "UPDATE [polihilton].[dbo].[Cleaning] SET status = 'In progress' WHERE r_id = '" + words[2] + "'";
                db1.Command(command);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
        }
        public void cleaned(System.Windows.Controls.ListBox l1)
        {
            char[] separator = { ' ' };
            try
            {
                string[] words = l1.SelectedItem.ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                String command = "UPDATE [polihilton].[dbo].[Cleaning] SET status = 'Cleaned' WHERE r_id = '" + words[2] + "'";
                db1.Command(command);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
            //update room as cleaned in status and refresh list_assigned_rooms();
        }

        


    }
}
