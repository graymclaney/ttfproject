using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class ConnectionString
    {
        public string cs {get; set;}
        public ConnectionString()
        {
            string server = "xq7t6tasopo9xxbs.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
            string database = "qfjsup6htjxrl82p";
            string port = "3306";
            string userName ="pwigqpmtiunqog9r";
            string password ="bkpblf5d26xjh0n7";

            cs = $"server = {server};user = {userName};database ={database};port={port};password={password}";
        }
    }
}