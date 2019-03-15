using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bitirme.Models
{
    public class otelresim
    {
        [Key]
        public int otelresimId { get; set; }
        public string otelresimAdi { get; set; }

        public Nullable<int> otelID { get; set; }
        public virtual otel otel { get; set; }
    }
}