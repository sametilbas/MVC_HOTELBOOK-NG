using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bitirme.Models
{
    public class kategori
    {

        [Key]
        public int kategoriID { get; set; }
        public string kategoriad { get; set; }
    }

}