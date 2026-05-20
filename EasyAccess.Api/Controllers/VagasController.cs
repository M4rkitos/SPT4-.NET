using EasyAccess.Application.DTOs;
using EasyAccess.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace EasyAccess.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VagasController : ControllerBase
    {
        private readonly IVagaService _vagaService;

        public VagasController(IVagaService vagaService)
        {
            _vagaService = vagaService;
        }

        // -------------------------------------------------------------------
        // 1. CREATE (POST)
        // -------------------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReservaVagaDto reservaDto)
        {
            if (reservaDto == null || string.IsNullOrEmpty(reservaDto.PlacaVeiculo))
            {
                return BadRequest(new { Message = "Erro de validação: O campo PlacaVeiculo é obrigatório." });
            }

            var resultado = await _vagaService.CriarReservaAsync(reservaDto);
            
            var localUrl = $"/api/Vagas/{resultado.Id}";
            return Created(localUrl, new { Id = resultado.Id, Message = "Reserva de vaga realizada com sucesso!", Data = resultado });
        }

        // -------------------------------------------------------------------
        // 2. READ (GET) - Consulta com Paginação, Filtros, Ordenação e HATEOAS
        // -------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? placa = null, [FromQuery] string? ordenacao = "Id")
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0 || pageSize > 50) pageSize = 10;

            // Consome o DTO estruturado de paginação vindo do Service
            var resultadoPaginado = await _vagaService.ObterVagasPaginadasAsync(page, pageSize, placa, ordenacao);
            
            int totalPages = (int)Math.Ceiling((double)resultadoPaginado.TotalItems / pageSize);

            var links = new List<object>
            {
                new { rel = "self", href = Url.Action(nameof(GetAll), null, new { page, pageSize, placa, ordenacao }, Request.Scheme), method = "GET" }
            };

            if (page < totalPages)
            {
                links.Add(new { rel = "nextPage", href = Url.Action(nameof(GetAll), null, new { page = page + 1, pageSize, placa, ordenacao }, Request.Scheme), method = "GET" });
            }

            if (page > 1)
            {
                links.Add(new { rel = "prevPage", href = Url.Action(nameof(GetAll), null, new { page = page - 1, pageSize, placa, ordenacao }, Request.Scheme), method = "GET" });
            }

            var responseEnvelope = new
            {
                TotalRegistros = resultadoPaginado.TotalItems,
                PaginaAtual = page,
                TotalPaginas = totalPages,
                TamanhoPagina = pageSize,
                Dados = resultadoPaginado.Items,
                Links = links
            };

            return Ok(responseEnvelope);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearch([FromQuery] SearchQueryDto query)
        {
            var resultado = await _vagaService.GetReservasAsync(query);
            return Ok(resultado);
        }

        // -------------------------------------------------------------------
        // 3. READ (GET) - Busca por ID com HATEOAS
        // -------------------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vaga = await _vagaService.ObterPorIdAsync(id);

            if (vaga == null)
            {
                return NotFound(new { Message = $"Reserva com ID {id} não encontrada." });
            }

            var links = new List<object>
            {
                new { rel = "self", href = Url.Action(nameof(GetById), null, new { id = vaga.Id }, Request.Scheme), method = "GET" },
                new { rel = "update", href = Url.Action(nameof(Put), null, new { id = vaga.Id }, Request.Scheme), method = "PUT" },
                new { rel = "delete", href = Url.Action(nameof(Delete), null, new { id = vaga.Id }, Request.Scheme), method = "DELETE" }
            };
            
            return Ok(new { Reserva = vaga, Links = links }); 
        }

        // -------------------------------------------------------------------
        // 4. UPDATE (PUT)
        // -------------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ReservaVagaDto reservaDto)
        {
            if (reservaDto == null) return BadRequest(new { Message = "Dados de atualização inválidos." });

            var atualizado = await _vagaService.AtualizarReservaAsync(id, reservaDto);
            if (!atualizado)
            {
                return NotFound(new { Message = $"Reserva com ID {id} não encontrada para atualização." });
            }
            return Ok(new { Message = $"Reserva {id} atualizada com sucesso." });
        }

        // -------------------------------------------------------------------
        // 5. DELETE (DELETE)
        // -------------------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletado = await _vagaService.DeletarReservaAsync(id);
            if (!deletado)
            {
                return NotFound(new { Message = $"Reserva com ID {id} não encontrada para exclusão." });
            }
            return NoContent(); 
        }
    }
}