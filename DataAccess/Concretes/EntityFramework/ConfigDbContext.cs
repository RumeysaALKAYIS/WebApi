using Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes.EntityFramework
{
    public class ConfigDbContext : DbContext
    {
        public ConfigDbContext(DbContextOptions<ConfigDbContext> options, IConfiguration configuration) :base(options)
        {
            Configuration = configuration;
        }
        public DbSet<Configuration> Configurations { get; set; }
        protected IConfiguration Configuration { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
               base.OnConfiguring(
                   optionsBuilder.UseMySql(Configuration.GetConnectionString("ConfigDb"), new MySqlServerVersion(new Version(8, 0, 31))));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Configuration>().ToTable("Configurations");
            modelBuilder.Entity<Configuration>().HasKey(c => c.Id);
            modelBuilder.Entity<Configuration>().Property(c => c.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Configuration>().Property(c => c.Type).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Configuration>().Property(c => c.Value).IsRequired();
            modelBuilder.Entity<Configuration>().Property(c => c.IsActive).IsRequired();
            modelBuilder.Entity<Configuration>().Property(c => c.ApplicationName).IsRequired().HasMaxLength(100);

            Configuration seedData = new Configuration
            {
                Id = 1,
                Name = "DefaultConfig",
                Type = "string",
                Value = "DefaultValue",
                IsActive = true,
                ApplicationName = "MyApp"
            };

            modelBuilder.Entity<Configuration>().HasData(seedData);
        }
    }
}
