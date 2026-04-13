namespace EasyAccess.Application.DTOs
{
    public class ReservaVagaDto
    {
        public int MoradorId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string PlacaVeiculo { get; set; }
    }
}