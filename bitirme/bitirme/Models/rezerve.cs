using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bitirme.Models
{
    public class rezerve
    {
        public int rezerveID { get; set; }
        public DateTime gtarih { get; set; }
        public DateTime ctarih { get; set; }
        public int kisi { get; set; }
        public Nullable<int> otelID { get; set; }
        public virtual otel otel { get; set; }
        public Nullable<int> odaID { get; set; }
        public virtual oteloda oteloda { get; set; }
        public bool Durum { get; set; }
    }
}