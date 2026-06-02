public class VagaReserva
{
    public int Id { get; set; }
    public int MoradorId { get; set; }
    public string PlacaVeiculo { get; set; } = string.Empty; 
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
}   