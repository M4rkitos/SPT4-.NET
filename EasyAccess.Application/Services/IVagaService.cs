using EasyAccess.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAccess.Application.Services
{
    public interface IVagaService
    {
        Task RealizarReservaVagaAsync(ReservaVagaDto reservaDto);
        // Ajustado para aceitar o DTO de busca que o Controller envia
        Task<IEnumerable<VagaReservaResponseDto>> GetReservasAsync(SearchQueryDto query); 
        Task<VagaReservaResponseDto> GetReservaByIdAsync(int id);
        Task UpdateReservaAsync(int id, ReservaVagaDto reservaDto);
        Task DeleteReservaAsync(int id);
    }
}