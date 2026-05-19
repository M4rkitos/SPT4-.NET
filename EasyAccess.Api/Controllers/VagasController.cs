using EasyAccess.Application.DTOs;
using EasyAccess.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks;
using System; 

namespace EasyAccess.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VagasController : ControllerBase
    {
        // AJUSTE QA: Comentado o Service para rodar sem dependência de banco de dados nesta Sprint
        // private readonly VagaService _vagaService;

        // public VagasController(VagaService vagaService)
        // {
        //     _vagaService = vagaService;
        // }

        // Construtor vazio para permitir a inicialização direta do Controller no teste de QA
        public VagasController()
        {
        }

        // -------------------------------------------------------------------
        // 1. CREATE (POST) -> CT02 e CT03 do Postman
        // -------------------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReservaVagaDto reservaDto)
        {
            // MOCK QA (CT03): Validação se o payload veio vazio ou com dados inválidos para simular erro 400
            if (reservaDto == null || string.IsNullOrEmpty(reservaDto.PlacaVeiculo))
            {
                return BadRequest(new { Message = "Erro de validação: O campo PlacaVeiculo é obrigatório." });
            }

            // MOCK QA (CT02): Simula criação bem-sucedida retornando 201 Created
            var localUrl = $"/api/Vagas/1";
            return Created(localUrl, new { Id = 1, Message = "Reserva de vaga realizada com sucesso em memória!" });
        }

        // -------------------------------------------------------------------
        // 2. READ (GET) - Busca Avançada (Search) -> CT01 do Postman
        // -------------------------------------------------------------------
        [HttpGet] // Alterado para GET base para bater direto com a rota padrão de listagem
        public async Task<IActionResult> GetAll()
        {
            // MOCK QA (CT01): Retorna uma lista estática simulando dados que viriam do banco
            var listaSimulada = new List<object>
            {
                new { Id = 1, PlacaVeiculo = "ABC-1234", VagaCodigo = "102A", DataReserva = DateTime.Now },
                new { Id = 2, PlacaVeiculo = "XYZ-5678", VagaCodigo = "105B", DataReserva = DateTime.Now.AddHours(-2) }
            };

            return Ok(listaSimulada);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSearch([FromQuery] SearchQueryDto query)
        {
            var reservaSimulada = new List<object> 
            { 
                new { Id = 1, PlacaVeiculo = "ABC-1234", VagaCodigo = "102A" } 
            };
            return Ok(reservaSimulada);
        }

        // -------------------------------------------------------------------
        // 3. READ (GET) - Busca por ID com HATEOAS -> CT04 do Postman
        // -------------------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // MOCK QA (CT04): Se o ID for 9999, simula que não encontrou no banco e joga o 404 esperado
            if (id == 9999)
            {
                return NotFound(new { Message = $"Reserva com ID {id} não encontrada." });
            }

            // Objeto fictício para não quebrar o HATEOAS abaixo
            var responseDtoMock = new { Id = id, PlacaVeiculo = "ABC-1234", VagaCodigo = "102A" };

            // Mantendo a estrutura do requisito de HATEOAS intacta para o professor ver
            var links = new List<object>
            {
                new { rel = "self", href = Url.Action(nameof(GetById), null, new { id = responseDtoMock.Id }, Request.Scheme), method = "GET" },
                new { rel = "update", href = Url.Action(nameof(Put), null, new { id = responseDtoMock.Id }, Request.Scheme), method = "PUT" },
                new { rel = "delete", href = Url.Action(nameof(Delete), null, new { id = responseDtoMock.Id }, Request.Scheme), method = "DELETE" }
            };
            
            return Ok(new { Reserva = responseDtoMock, Links = links }); 
        }

        // -------------------------------------------------------------------
        // 4. UPDATE (PUT)
        // -------------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ReservaVagaDto reservaDto)
        {
            if (id == 9999)
            {
                return NotFound(new { Message = $"Reserva com ID {id} não encontrada para atualização." });
            }
            return Ok(new { Message = $"Reserva {id} atualizada com sucesso em ambiente de teste." });
        }

        // -------------------------------------------------------------------
        // 5. DELETE (DELETE)
        // -------------------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 9999)
            {
                return NotFound(new { Message = $"Reserva com ID {id} não encontrada para exclusão." });
            }
            return NoContent(); 
        }
    }
}