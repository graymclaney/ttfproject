using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Database
{
    public class SellFurniture : ISellFurniture
    {
        public void ChangeSoldStatus(Furniture value)
        {
            try
            {
                ConnectionString myConnection = new ConnectionString();
                string cs = myConnection.cs; 
                using MySqlConnection con = new MySqlConnection(cs); 
                con.Open();

                using var cmd = new MySqlCommand(); 

                cmd.Connection = con;
                cmd.CommandText = @"update furniture set furniture_sold = @sold where furniture_id = @id";
                cmd.Parameters.AddWithValue("@sold", value.Sold);
                cmd.Parameters.AddWithValue("@id", value.Id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("Put successful");
            }
            catch(Exception ex)
            {
                System.Console.WriteLine("Put error occured" + ex);
            }
        }
    }
}