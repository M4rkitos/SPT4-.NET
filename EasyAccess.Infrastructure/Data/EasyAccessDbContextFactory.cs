using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EasyAccess.Infrastructure.Data
{
    public class EasyAccessDbContextFactory : IDesignTimeDbContextFactory<EasyAccessDbContext>
    {
        public EasyAccessDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EasyAccessDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EasyAccess;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new EasyAccessDbContext(optionsBuilder.Options);
        }
    }
}