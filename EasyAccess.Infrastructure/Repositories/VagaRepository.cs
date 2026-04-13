using EasyAccess.Domain.Entities;
using EasyAccess.Domain.Repositories;
using EasyAccess.Infrastructure.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EasyAccess.Infrastructure.Repositories
{
    public class VagaRepository : IVagaRepository
    {
        private readonly EasyAccessDbContext _context;

        public VagaRepository(EasyAccessDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(VagaReserva reserva)
        {
            await _context.VagasReservas.AddAsync(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task<VagaReserva> ObterPorIdAsync(int id)
        {
            return await _context.VagasReservas.FindAsync(id);
        }
        
    }
}