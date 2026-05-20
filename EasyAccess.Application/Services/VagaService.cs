using EasyAccess.Application.DTOs;
using EasyAccess.Domain.Entities;
using EasyAccess.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace EasyAccess.Application.Services
{
    public class VagaService : IVagaService
    {
        private readonly IVagaRepository _vagaRepository;

        public VagaService(IVagaRepository vagaRepository)
        {
            _vagaRepository = vagaRepository;
        }

        // -------------------------------------------------------------------
        // 1. CREATE (POST)
        // -------------------------------------------------------------------
        public async Task<VagaReservaResponseDto> CriarReservaAsync(ReservaVagaDto reservaDto)
        {
            if (reservaDto.DataInicio >= reservaDto.DataFim)
            {
                throw new ArgumentException("A data de início deve ser anterior à data de fim.");
            }

            var novaReserva = new VagaReserva
            {
                MoradorId = reservaDto.MoradorId,
                DataInicio = reservaDto.DataInicio,
                DataFim = reservaDto.DataFim,
                PlacaVeiculo = reservaDto.PlacaVeiculo
            };

            await _vagaRepository.AdicionarAsync(novaReserva);

            return new VagaReservaResponseDto
            {
                Id = novaReserva.Id,
                PlacaVeiculo = novaReserva.PlacaVeiculo
            };
        }

        public async Task RealizarReservaVagaAsync(ReservaVagaDto reservaDto)
        {
            if (reservaDto.DataInicio >= reservaDto.DataFim)
            {
                throw new ArgumentException("A data de início deve ser anterior à data de fim.");
            }

            var novaReserva = new VagaReserva
            {
                MoradorId = reservaDto.MoradorId,
                DataInicio = reservaDto.DataInicio,
                DataFim = reservaDto.DataFim,
                PlacaVeiculo = reservaDto.PlacaVeiculo
            };

            await _vagaRepository.AdicionarAsync(novaReserva);
        }

        // -------------------------------------------------------------------
        // 2. READ (GET) - Lista Paginada com Filtro (Corrigido para usar PagedResultDto)
        // -------------------------------------------------------------------
        public async Task<PagedResultDto<VagaReservaResponseDto>> ObterVagasPaginadasAsync(int page, int pageSize, string? placa, string? ordenacao)
        {
            // Busca a tupla vinda do IVagaRepository
            var resultadoRepo = await _vagaRepository.ObterPaginadoAsync(page, pageSize, placa, ordenacao);

            // Mapeia as entidades de domínio para DTO de resposta de forma explícita
            var dtos = resultadoRepo.Items.Select(e => new VagaReservaResponseDto
            {
                Id = e.Id,
                PlacaVeiculo = e.PlacaVeiculo
            }).ToList();

            return new PagedResultDto<VagaReservaResponseDto>
            {
                Items = dtos,
                TotalItems = resultadoRepo.TotalCount
            };
        }

        public async Task<IEnumerable<VagaReservaResponseDto>> GetReservasAsync(SearchQueryDto query)
        {
            return await Task.FromResult(new List<VagaReservaResponseDto>());
        }

        // -------------------------------------------------------------------
        // 3. READ (GET) - Por ID
        // -------------------------------------------------------------------
        public async Task<VagaReservaResponseDto> ObterPorIdAsync(int id)
        {
            var reserva = await _vagaRepository.ObterPorIdAsync(id);
            if (reserva == null) return null!;

            return new VagaReservaResponseDto
            {
                Id = reserva.Id,
                PlacaVeiculo = reserva.PlacaVeiculo
            };
        }

        public async Task<VagaReservaResponseDto> GetReservaByIdAsync(int id)
        {
            return await ObterPorIdAsync(id);
        }

        // -------------------------------------------------------------------
        // 4. UPDATE (PUT)
        // -------------------------------------------------------------------
        public async Task<bool> AtualizarReservaAsync(int id, ReservaVagaDto reservaDto)
        {
            var reservaExistente = await _vagaRepository.ObterPorIdAsync(id);
            if (reservaExistente == null) return false;

            reservaExistente.MoradorId = reservaDto.MoradorId;
            reservaExistente.DataInicio = reservaDto.DataInicio;
            reservaExistente.DataFim = reservaDto.DataFim;
            reservaExistente.PlacaVeiculo = reservaDto.PlacaVeiculo;

            await _vagaRepository.AtualizarAsync(reservaExistente);
            return true;
        }

        public async Task UpdateReservaAsync(int id, ReservaVagaDto reservaDto)
        {
            await AtualizarReservaAsync(id, reservaDto);
        }

        // -------------------------------------------------------------------
        // 5. DELETE (DELETE)
        // -------------------------------------------------------------------
        public async Task<bool> DeletarReservaAsync(int id)
        {
            var reservaExistente = await _vagaRepository.ObterPorIdAsync(id);
            if (reservaExistente == null) return false;

            await _vagaRepository.DeletarAsync(id);
            return true;
        }

        public async Task DeleteReservaAsync(int id)
        {
            await DeletarReservaAsync(id);
        }
    }
}