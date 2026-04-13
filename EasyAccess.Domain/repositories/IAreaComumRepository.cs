using System.Threading.Tasks;
using EasyAccess.Domain.Entities;

namespace EasyAccess.Domain.Repositories
{
    public interface IAreaComumRepository
    {
        Task AdicionarAsync(AreaComumReserva reserva);
        Task<AreaComumReserva> ObterPorIdAsync(int id);
    }
}