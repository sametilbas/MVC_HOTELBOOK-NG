using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bitirme.Models
{
    public class RezerveView
    {
        public IEnumerable<rezerve> rezerves { get; set; }
        public IEnumerable<otel> otels { get; set; }
        public IEnumerable<oteloda> otelodas { get; set; }
        public IEnumerable<User> users { get; set; }
    }
}