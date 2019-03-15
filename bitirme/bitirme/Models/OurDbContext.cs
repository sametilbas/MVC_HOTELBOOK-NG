using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace bitirme.Models
{
    public class OurDbContext:DbContext
    {
        public OurDbContext() : base("identity")
        {
            Database.SetInitializer<OurDbContext>(new DropCreateDatabaseIfModelChanges<OurDbContext>());
        }
        public DbSet<otel> otels { get; set; }
        public DbSet<kategori> kategoris { get; set; }
        public DbSet<ozellik> ozelliks { get; set; }
        public DbSet<otelresim> otelresims { get; set; }
        public DbSet<oteloda> otelodas { get; set; }
        public DbSet<User> users { get; set; }
    }
   
}