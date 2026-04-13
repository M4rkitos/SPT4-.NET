using EasyAccess.Application.DTOs;
using EasyAccess.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq; // Necessário para .Any() e .Select()
using System; // Para o uso de ArgumentException

namespace EasyAccess.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VagasController : ControllerBase
    {
        private readonly VagaService _vagaService;

        public VagasController(VagaService vagaService)
        {
            _vagaService = vagaService;
        }

        // -------------------------------------------------------------------
        // 1. CREATE (POST)
        // -------------------------------------------------------------------

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReservaVagaDto reservaDto)
        {
            await _vagaService.RealizarReservaVagaAsync(reservaDto);
            // Retorna 200 OK ou idealmente 201 Created com a URL do novo recurso
            return Ok(); 
        }

        // -------------------------------------------------------------------
        // 2. READ (GET) - Busca Avançada (Search)
        // -------------------------------------------------------------------

        [HttpGet("search")]
        public async Task<IActionResult> GetSearch([FromQuery] SearchQueryDto query)
        {
            var reservas = await _vagaService.GetReservasAsync(query);
            
            if (reservas == null || !reservas.Any())
            {
                return NotFound("Nenhuma reserva encontrada com os critérios fornecidos."); // Retorna 404
            }
            
            return Ok(reservas); 
        }

        // -------------------------------------------------------------------
        // 3. READ (GET) - Busca por ID com HATEOAS (Requisito 15 pts)
        // -------------------------------------------------------------------

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // O serviço deve retornar o DTO de resposta (VagaReservaResponseDto)
            var responseDto = await _vagaService.GetReservaByIdAsync(id); 
            
            if (responseDto == null)
            {
                return NotFound($"Reserva com ID {id} não encontrada."); // Retorna 404
            }
            
            // Adicionar os links HATEOAS (Guia o cliente para próximas ações)
            var links = new List<object>
            {
                // Link para o próprio recurso
                new { rel = "self", href = Url.Action(nameof(GetById), null, new { id = responseDto.Id }, Request.Scheme), method = "GET" },
                // Link para a ação de atualização
                new { rel = "update", href = Url.Action(nameof(Put), null, new { id = responseDto.Id }, Request.Scheme), method = "PUT" },
                // Link para a ação de exclusão
                new { rel = "delete", href = Url.Action(nameof(Delete), null, new { id = responseDto.Id }, Request.Scheme), method = "DELETE" }
            };
            
            // Retorna o DTO junto com os links HATEOAS
            return Ok(new { Reserva = responseDto, Links = links }); 
        }

        // -------------------------------------------------------------------
        // 4. UPDATE (PUT) - Atualizar
        // -------------------------------------------------------------------

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ReservaVagaDto reservaDto)
        {
            try
            {
                await _vagaService.UpdateReservaAsync(id, reservaDto);
                return Ok(new { Message = $"Reserva {id} atualizada com sucesso." }); // Retorna 200 OK
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Reserva com ID {id} não encontrada para atualização.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Retorna 400 Bad Request
            }
        }

        // -------------------------------------------------------------------
        // 5. DELETE (DELETE) - Excluir
        // 

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _vagaService.DeleteReservaAsync(id);
                return NoContent(); // Retorna 204 No Content
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Reserva com ID {id} não encontrada para exclusão.");
            }
        }
    }
}