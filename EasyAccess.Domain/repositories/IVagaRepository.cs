using System.Threading.Tasks;
using System.Collections.Generic;
using EasyAccess.Domain.Entities;

namespace EasyAccess.Domain.Repositories
{
    public interface IVagaRepository
    {
        Task AdicionarAsync(VagaReserva reserva);
        Task<VagaReserva> ObterPorIdAsync(int id);
        
        // Métodos de atualização e deleção que o Service consome
        Task AtualizarAsync(VagaReserva reserva);
        Task DeletarAsync(int id);

        // Assinatura de paginação e filtros que vai direto para a infraestrutura do EF Core
        Task<(IEnumerable<VagaReserva> Items, int TotalCount)> ObterPaginadoAsync(int page, int pageSize, string? placa, string? ordenacao);
    }
}