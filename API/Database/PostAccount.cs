using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Database
{
    public class PostAccount : IPostAccount
    {
        void IPostAccount.PostAccount(Account value)
        {
            try
            {
                ConnectionString myConnection = new ConnectionString();
                string cs = myConnection.cs;  
                using MySqlConnection con = new MySqlConnection(cs); 
                con.Open();

                using var cmd = new MySqlCommand(); 

                cmd.Connection = con;
                cmd.CommandText = @"insert into accounts(account_id, account_username, account_email, account_password, account_admin) 
                                VALUES(@id, @username, @email, @password, @admin)";
                System.Console.WriteLine(value.ToString());
                cmd.Parameters.AddWithValue("@id", value.Id);
                cmd.Parameters.AddWithValue("@username", value.Username);
                cmd.Parameters.AddWithValue("@email", value.Email);
                cmd.Parameters.AddWithValue("@password", value.Password);
                cmd.Parameters.AddWithValue("@admin", value.Admin);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("Post successful");
            }
            catch
            {
                System.Console.WriteLine("Post error occured");
            }
        }
    }
}