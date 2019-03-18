using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bitirme.Models
{
    public class sehir
    {
        [Key]
        public int sehirID { get; set; }
        public string sehirAd { get; set; }
        public string sehirresim { get; set; }
    }
}