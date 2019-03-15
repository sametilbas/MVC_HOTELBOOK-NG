using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace bitirme.Models
{
    public class ozellik
    {
        [Key]
        public int ozellikID { get; set; }
        public Nullable<int> otelID { get; set; }     
        public virtual otel otel { get; set; }

        public string kategoriID { get; set; }
        [NotMapped]
        public IEnumerable<kategori> kategoris { get; set; }
        [NotMapped]
         public string[] selectedID { get; set; }
    }
}