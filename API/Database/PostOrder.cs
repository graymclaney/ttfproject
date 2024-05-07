using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

namespace API.Database
{
    public class PostOrder : IPostOrder
    {
        void IPostOrder.PostOrder(OrderForm value)
        {
            try
            {
                ConnectionString myConnection = new ConnectionString();
                string cs = myConnection.cs;  
                using MySqlConnection con = new MySqlConnection(cs); 
                con.Open();

                using var cmd = new MySqlCommand(); 

                cmd.Connection = con;
                cmd.CommandText = @"insert into orderform(order_id, furniture_id, buyer_id, pickup_date, price, order_fname, order_lname, order_location, order_phone) 
                VALUES(@orderid, (select furniture_id from furniture where furniture_id = @furnid limit 1), (select account_id from accounts where account_id = @buyerid limit 1),
                @date, @price, @fname, @lname, @location, @phone)";
                System.Console.WriteLine(value.ToString());
                cmd.Parameters.AddWithValue("@orderid", value.Id);
                cmd.Parameters.AddWithValue("@furnid", value.FurnitureId);
                cmd.Parameters.AddWithValue("@buyerid", value.BuyerId);
                cmd.Parameters.AddWithValue("@date", value.PickupDate);
                cmd.Parameters.AddWithValue("@price", value.Price);
                cmd.Parameters.AddWithValue("@fname", value.FName);
                cmd.Parameters.AddWithValue("@lname", value.LName);
                cmd.Parameters.AddWithValue("@location", value.Location);
                cmd.Parameters.AddWithValue("@phone", value.Phone);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("Post successful");
            }
            catch(Exception ex)
            {
                System.Console.WriteLine("Post error occured" + ex);
            }
        }
    }
}