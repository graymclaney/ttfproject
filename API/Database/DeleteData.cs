using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Database
{
    public class DeleteData : IDeleteData
    {
        void IDeleteData.DeleteData(int id)
        {
            try
            {
                ConnectionString myConnection = new ConnectionString();
                string cs = myConnection.cs; //cs - connection string  
                using MySqlConnection con = new MySqlConnection(cs); //con - connection
                con.Open();

                using var cmd = new MySqlCommand(); //sends command to the established connection

                cmd.Connection = con;
                cmd.CommandText = @"delete from orderform where furniture_id = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                cmd.CommandText = @"delete from furniture where furniture_id = @id2";
                cmd.Parameters.AddWithValue("@id2", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("Delete successful");
            }
            catch(Exception ex)
            {
                System.Console.WriteLine("Delete error occured" + ex);
            }
        }
    }
}