using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bitirme.Models
{
    public class oteldetayview
    {
        public IEnumerable<otel> otel { get; set; }
        public IEnumerable<otelresim> otelresim { get; set; }
        public IEnumerable<oteloda> oteloda { get; set; }
        public IEnumerable<ozellik> ozelliks { get; set; }
        public IEnumerable<kategori> kategoris { get; set; }
    }
}