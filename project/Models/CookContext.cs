using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project.Models
{
    public class CookContext : DbContext
    {
        public DbSet<Cook> Cooks { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Qualification>().HasMany(c => c.cooks)
                .WithMany(s => s.qualifications)
                .Map(t => t.MapLeftKey("Qualification_Id")
                .MapRightKey("Cook_Id")
                .ToTable("QualificationCooks"));
        }


    }
}