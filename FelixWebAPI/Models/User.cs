using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FelixWebAPI.Models
{
    public class User
    {
        public string user_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string phone { get; set; }
        public string dob { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public string profile_pic { get; set; }

    }
}