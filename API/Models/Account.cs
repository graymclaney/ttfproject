using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Account
    {
        public int Id{get; set;}

        public string Username{get; set;}
        
        public string Email{get; set;}

        public string Password{get; set;}

        public bool Admin{get; set;}

        // public string FName{get; set;}

        // public string LName{get; set;}

        // public string PhoneNumber{get; set;}

        // public string Location{get; set;}

        // public string Payment{get; set;}
    }

    // 	account_id int auto_increment primary key,
    // account_username varchar(255) not null,
    // account_password varchar(255) not null,
    // account_admin_id bool not null default false,
	// account_fname varchar(25),
    // account_lname varchar(25),
    // account_phone_number varchar(10),
    // account_location varchar(50),
    // account_payment_info varchar(50)
}