using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Database
{
    public class ReadAccountData : IGetAllAccounts, IGetAccount
    {

        public List<Account> GetAllAccounts()
        {
            try
            {
                ConnectionString myConnection = new ConnectionString();
                string cs = myConnection.cs; 
                using MySqlConnection con = new MySqlConnection(cs); 
                con.Open();

                string stm = "select * from accounts"; 
                using var cmd = new MySqlCommand(stm, con); 

                using MySqlDataReader rdr = cmd.ExecuteReader();

                List<Account> allAccounts = new List<Account>(); 
                while(rdr.Read()) 
                {
                    allAccounts.Add(new Account()
                    {   
                        Id = rdr.GetInt32(0), Username = rdr.GetString(1), Email=rdr.GetString(2),
                        Password = rdr.GetString(3), Admin = rdr.GetBoolean(4)
                    }); 
                }

                System.Console.WriteLine("Retrieved Successfully");
                return allAccounts;
            }
            catch(Exception ex)
            {
                System.Console.WriteLine("Retrieval Failed"  + ex.Message);
                return null;
            }
        }

        public Account GetAccount(int id)
        {
           try
            {
                ConnectionString myConnection = new ConnectionString();
                string cs = myConnection.cs;
                using MySqlConnection con = new MySqlConnection(cs); 
                con.Open();

                string stm = "select * from accounts where account_id = @id"; 
                using var cmd = new MySqlCommand(stm, con); 

                cmd.Parameters.AddWithValue("@id",id);
                cmd.Prepare(); 

                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();


                System.Console.WriteLine("Retrieved Successfully");

                return new Account()
                {
                    Id = rdr.GetInt32(0), Username = rdr.GetString(1), Email=rdr.GetString(2),
                    Password = rdr.GetString(3), Admin = rdr.GetBoolean(4)
                };
            }
            catch
            {
                System.Console.WriteLine("Retrieval Failed");
                return null;
            }
        }

    }
}