using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace bitirme.Models
{
    public class oteloda
    {
        [Key]
        public int odaID { get; set; }
        public Nullable<int> otelID { get; set; }
        public virtual otel otel { get; set; }
        public string odaAdi { get; set; }
        public int odakisi { get; set; }
        public int odaucret { get; set; }
        public string aciklama { get; set; }
    }
}