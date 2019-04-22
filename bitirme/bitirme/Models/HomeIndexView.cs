using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bitirme.Models
{
    public class HomeIndexView
    {
        public DateTime gtarih { get; set; }
        public DateTime ctarih { get; set; }
        public int kisi { get; set; }
        public int otelID { get; set; }
        public string otelad { get; set; }
        public IEnumerable<otel> otel { get; set; }
        public IEnumerable<sehir> sehir { get; set; }
        public IEnumerable<oteloda> oteloda { get; set; }
        public IEnumerable<rezerve> rezer { get; set; }
    }
}