using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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

            return new ConfigDbContext(optionsBuilder.Options, null);
        }
    }
}