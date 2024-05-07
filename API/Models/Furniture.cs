using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace API.Models
{
    public class Furniture 
    {
        public int Id{get; set;}

        public string Type{get; set;}

        public string Quality{get;set;}

        public string City{get;set;}

        public bool Sold{get; set;}

        public int Price{get;set;}

        public string Image{get; set;}

        public int SellerId{get; set;}

    }
}