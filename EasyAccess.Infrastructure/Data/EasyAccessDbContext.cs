using EasyAccess.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyAccess.Infrastructure.Data
{
    public class EasyAccessDbContext : DbContext
    {
        public EasyAccessDbContext(DbContextOptions<EasyAccessDbContext> options)
            : base(options)
        {
        }

        public DbSet<VagaReserva> VagasReservas { get; set; }
        public DbSet<AreaComumReserva> AreasComunsReservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}