using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bitirme.Models
{
    public class HomeIndexView
    {
        public IEnumerable<otel> otel { get; set; }
        public IEnumerable<sehir> sehir { get; set; }
        public IEnumerable<oteloda> oteloda { get; set; }
    }
}