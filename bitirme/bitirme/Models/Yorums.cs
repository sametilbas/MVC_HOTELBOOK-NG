using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bitirme.Models
{
    public class Yorums
    {
        [Key]
        public int yorumID { get; set; }
        public string yorum { get; set; }
        public double puan { get; set; }
        public Nullable<int> otelID { get; set; }
        public virtual otel otel { get; set; }
        public Nullable<int> userID { get; set; }
        public virtual User user { get; set; }
    }
}