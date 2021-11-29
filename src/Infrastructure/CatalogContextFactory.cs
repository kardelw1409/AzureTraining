using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace Microsoft.eShopWeb.Infrastructure
{
    public class CatalogContextFactory : IDesignTimeDbContextFactory<CatalogContext>
    {
        public CatalogContext CreateDbContext(string[] args)
        {
            var apiAssemblyPath = Path.Combine(Directory.GetCurrentDirectory());
            var configuration = new ConfigurationBuilder().SetBasePath(apiAssemblyPath).AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("CatalogConnection");
            var builder = new DbContextOptionsBuilder<CatalogContext>();
            builder.UseSqlServer(connectionString);
            return new CatalogContext(builder.Options);
        }
    }
}
