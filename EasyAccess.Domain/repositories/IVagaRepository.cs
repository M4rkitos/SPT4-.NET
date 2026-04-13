using System.Threading.Tasks;
using EasyAccess.Domain.Entities;

namespace EasyAccess.Domain.Repositories
{
    public interface IVagaRepository
    {
        Task AdicionarAsync(VagaReserva reserva);
        Task<VagaReserva> ObterPorIdAsync(int id);
    }
}