using EasyAccess.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAccess.Application.Services
{
    public interface IVagaService
    {
        // Contratos consumidos pelo VagasController (Sprint 4 de .NET)
        Task<VagaReservaResponseDto> CriarReservaAsync(ReservaVagaDto reservaDto);
        Task<VagaReservaResponseDto> ObterPorIdAsync(int id);
        Task<bool> AtualizarReservaAsync(int id, ReservaVagaDto reservaDto);
        Task<bool> DeletarReservaAsync(int id);

        // Assinatura de paginação limpa e sem tuplas para o Controller
        Task<PagedResultDto<VagaReservaResponseDto>> ObterVagasPaginadasAsync(int page, int pageSize, string? placa, string? ordenacao);
        
        // Métodos originais e legados mantidos para conformidade de herança
        Task RealizarReservaVagaAsync(ReservaVagaDto reservaDto);
        Task<IEnumerable<VagaReservaResponseDto>> GetReservasAsync(SearchQueryDto query); 
        Task<VagaReservaResponseDto> GetReservaByIdAsync(int id);
        Task UpdateReservaAsync(int id, ReservaVagaDto reservaDto);
        Task DeleteReservaAsync(int id);
    }
}