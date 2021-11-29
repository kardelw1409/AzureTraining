using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;

namespace Microsoft.eShopWeb.Infrastructure
{
    public class AppIdentityDbContextFactory : IDesignTimeDbContextFactory<AppIdentityDbContext>
    {
        public AppIdentityDbContext CreateDbContext(string[] args)
        {
            var apiAssemblyPath = Path.Combine(Directory.GetCurrentDirectory());
            var configuration = new ConfigurationBuilder().SetBasePath(apiAssemblyPath).AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("IdentityConnection");
            var builder = new DbContextOptionsBuilder<AppIdentityDbContext>();
            builder.UseSqlServer(connectionString);
            return new AppIdentityDbContext(builder.Options);
        }
    }
}
