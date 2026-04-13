using EasyAccess.Application.DTOs;
using EasyAccess.Domain.Entities;
using EasyAccess.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace EasyAccess.Application.Services
{
    public class VagaService : IVagaService
    {
        private readonly IVagaRepository _vagaRepository;

        public VagaService(IVagaRepository vagaRepository)
        {
            _vagaRepository = vagaRepository;
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

        // Aceitando o SearchQueryDto para o Controller ficar feliz
        public async Task<IEnumerable<VagaReservaResponseDto>> GetReservasAsync(SearchQueryDto query)
        {
            return await Task.FromResult(new List<VagaReservaResponseDto>());
        }

        public async Task<VagaReservaResponseDto> GetReservaByIdAsync(int id)
        {
            return await Task.FromResult(new VagaReservaResponseDto());
        }

        public async Task UpdateReservaAsync(int id, ReservaVagaDto reservaDto)
        {
            await Task.CompletedTask;
        }

        public async Task DeleteReservaAsync(int id)
        {
            await Task.CompletedTask;
        }
    }
}