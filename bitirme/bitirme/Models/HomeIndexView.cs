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
        public string search { get; set; }
        public int otelID { get; set; }
        public int odaID { get; set; }
        public int userID { get; set; }
        public string otelad { get; set; }
        public int gunsayisi { get; set; }
        public int ucret { get; set; }
        public IEnumerable<otel> otel { get; set; }
        public IEnumerable<sehir> sehir { get; set; }
        public IEnumerable<oteloda> oteloda { get; set; }
        public IEnumerable<rezerve> rezer { get; set; }
    }
}