using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication_Parkovka.Models;
namespace WebApplication_Parkovka.Models
{
    public class BazaContext:DbContext
    {
        public DbSet<Rollar> Rollar { get; set; }
        public DbSet<Userlar> Userlar { get; set; }
        public DbSet<Narx> Narx { get; set; }
        public DbSet<Joylar> Joylar { get; set; }
        public DbSet<JadvalFoy> JadvalFoy { get; set; }
        public DbSet<JadvalXodim> JadvalXodim { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<WebApplication_Parkovka.Models.Xodim> Xodims { get; set; }
    }
    public class BazaInit : CreateDatabaseIfNotExists<BazaContext>
    {
        protected override void Seed(BazaContext context)
        {
            base.Seed(context);
            context.Rollar.Add(new Rollar { Nomi = "Admin" });
            context.SaveChanges();
        }
    }
}