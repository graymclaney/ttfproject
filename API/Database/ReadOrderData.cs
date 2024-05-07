using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Database
{
    public class ReadOrderData : IGetAllOrders, IGetOrder
    {
        public List<OrderForm> GetAllOrders()
        {
            try
            {
                ConnectionString myConnection = new ConnectionString();
                string cs = myConnection.cs; 
                using MySqlConnection con = new MySqlConnection(cs); 
                con.Open();

                string stm = "select * from orderform"; 
                using var cmd = new MySqlCommand(stm, con); 

                using MySqlDataReader rdr = cmd.ExecuteReader();

                List<OrderForm> allOrders = new List<OrderForm>(); 
                while(rdr.Read()) 
                {
                    allOrders.Add(new OrderForm()
                    {   
                        Id = rdr.GetInt32(0), FurnitureId = rdr.GetInt32(1), BuyerId = rdr.GetInt32(2), 
                        PickupDate = rdr.GetDateTime(3), Price = rdr.GetInt32(4), FName = rdr.GetString(5),
                        LName = rdr.GetString(6), Location = rdr.GetString(7), Phone = rdr.GetString(8)
                    }); 
                }

                System.Console.WriteLine("Retrieved Successfully");
                return allOrders;
            }
            catch(Exception ex)
            {
                System.Console.WriteLine("Retrieval Failed"  + ex.Message);
                return null;
            }
        }

        public OrderForm GetOrder(int id)
        {
            try
            {
                ConnectionString myConnection = new ConnectionString();
                string cs = myConnection.cs; 
                using MySqlConnection con = new MySqlConnection(cs); 
                con.Open();

                string stm = "select * from orderform where order_id = @id"; 
                using var cmd = new MySqlCommand(stm, con); 

                cmd.Parameters.AddWithValue("@id",id); 
                cmd.Prepare(); 

                MySqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();


                System.Console.WriteLine("Retrieved Successfully");

                return new OrderForm()
                {
                    Id = rdr.GetInt32(0), FurnitureId = rdr.GetInt32(1), BuyerId = rdr.GetInt32(2), 
                    PickupDate = rdr.GetDateTime(3), Price = rdr.GetInt32(4), FName = rdr.GetString(5),
                    LName = rdr.GetString(6), Location = rdr.GetString(7), Phone = rdr.GetString(8)
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