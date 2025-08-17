using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concretes.EntityFramework
{
    public class ConfigDbContextFactory : IDesignTimeDbContextFactory<ConfigDbContext>
    {
        public ConfigDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConfigDbContext>();
            optionsBuilder.UseMySql(
                "server=localhost;database=WebApiDb;user=root;password=SeninSifren;",
                new MySqlServerVersion(new Version(8, 0, 31))
            );
            IConfiguration configuration = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json") 
           .Build();

            return new ConfigDbContext(optionsBuilder.Options, configuration);
        }
    }
}