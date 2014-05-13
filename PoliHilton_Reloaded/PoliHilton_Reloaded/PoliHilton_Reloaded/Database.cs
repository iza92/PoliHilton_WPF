using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows;
using System.Data;

namespace PoliHilton_Reloaded
{//Mostly all functions that we need are implementedE
    public class Database
    {
        SqlConnection myConnection;
        public Database()
        {
            this.myConnection = new SqlConnection(@"Data Source=IZA;Initial Catalog=Polihilton;Integrated Security=True");
        }

        public void init()
        {
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error Connection" + e.ToString());
            }
        }

        public void Command(String command)
        {

            using (SqlCommand myCommand = new SqlCommand(command, this.myConnection))
            {
                try
                {
                    myCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error Insert" + e.ToString());
                }
            }

        }


        public DataSet Read(String command)
        {
            DataSet dsUniv = new DataSet();
            SqlDataAdapter daUniv = new SqlDataAdapter();
            daUniv = new SqlDataAdapter(command, myConnection);
            daUniv.Fill(dsUniv, "table");
            return dsUniv;
        }


        public void Close()
        {
            try
            {
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}