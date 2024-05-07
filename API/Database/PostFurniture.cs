using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;

namespace API.Database
{
    public class PostFurniture : IPostFurniture
    {
        void IPostFurniture.PostFurniture(Furniture value)
        {
            try
            {
                ConnectionString myConnection = new ConnectionString();
                string cs = myConnection.cs;  
                using MySqlConnection con = new MySqlConnection(cs); 
                con.Open();

                using var cmd = new MySqlCommand(); 

                cmd.Connection = con;
                cmd.CommandText =
                @"insert into furniture(furniture_id, furniture_type, furniture_quality, furniture_city, furniture_sold, furniture_price, furniture_image, furniture_seller_id) 
                VALUES(@id, @type, @quality, @city, @sold, @price, @image, (select account_id from accounts where account_id = @sellid limit 1))";
                System.Console.WriteLine(value.ToString());
                cmd.Parameters.AddWithValue("@id", value.Id);
                cmd.Parameters.AddWithValue("@type", value.Type);
                cmd.Parameters.AddWithValue("@quality", value.Quality);
                cmd.Parameters.AddWithValue("@city", value.City);
                cmd.Parameters.AddWithValue("@sold", value.Sold);
                cmd.Parameters.AddWithValue("@price", value.Price);
                cmd.Parameters.AddWithValue("@image", value.Image);
                cmd.Parameters.AddWithValue("@sellid", value.SellerId);
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