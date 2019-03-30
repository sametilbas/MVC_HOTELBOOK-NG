using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace bitirme.Models
{
    public class otelresim
    {
        [Key]
        public int otelresimId { get; set; }
        public string otelresimAdi { get; set; }
        [NotMapped]
        public HttpPostedFileBase resim { get; set; }
        public Nullable<int> otelID { get; set; }
        public virtual otel otel { get; set; }
    }
}