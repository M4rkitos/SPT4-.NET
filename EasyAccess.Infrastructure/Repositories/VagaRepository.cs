using EasyAccess.Domain.Entities;
using EasyAccess.Domain.Repositories;
using EasyAccess.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            await _context.Set<VagaReserva>().AddAsync(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task<VagaReserva> ObterPorIdAsync(int id)
        {
            var resultado = await _context.Set<VagaReserva>().FindAsync(id);
            return resultado!;
        }

        public async Task AtualizarAsync(VagaReserva reserva)
        {
            _context.Set<VagaReserva>().Update(reserva);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(int id)
        {
            var reserva = await ObterPorIdAsync(id);
            if (reserva != null)
            {
                _context.Set<VagaReserva>().Remove(reserva);
                await _context.SaveChangesAsync();
            }
        }

        // Assinatura de paginação explícita alinhada perfeitamente com o contrato da interface de domínio
        public async Task<(IEnumerable<VagaReserva> Items, int TotalCount)> ObterPaginadoAsync(int page, int pageSize, string? placa, string? ordenacao)
        {
            var query = _context.Set<VagaReserva>().AsQueryable();

            if (!string.IsNullOrEmpty(placa))
            {
                query = query.Where(v => v.PlacaVeiculo.Contains(placa));
            }

            int totalCount = await query.CountAsync();

            query = query.OrderBy(v => v.Id);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }
    }
}