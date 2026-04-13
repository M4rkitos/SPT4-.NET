namespace EasyAccess.Domain.Entities
{
    public class VagaReserva
    {
        public int Id { get; set; }
        public int MoradorId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string PlacaVeiculo { get; set; }
        public decimal ValorCobrado { get; set; }
    }
}