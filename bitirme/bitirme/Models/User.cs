using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bitirme.Models
{
    public class User
    {
        [Key]
        public int userID { get; set; }
        public string userName { get; set; }
        public string userLastName { get; set; }
        public int userPhone { get; set; }
        public string userEmail { get; set; }
        public string userNickName { get; set; }
        public string userPassword { get; set; }
    }
}