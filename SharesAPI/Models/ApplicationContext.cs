using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SharesAPI.Models
{
    public class ApplicationContext : DbContext
    {
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Groups>().OwnsOne(x => x.GroupName);
        }*/
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Shares> Shares { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
