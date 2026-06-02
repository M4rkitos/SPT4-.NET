public class ReserVagaDto // Ou o DTO de entrada que você usar para a rota de criação
{
    public int MoradorId { get; set; }
    public string PlacaVeiculo { get; set; } = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataFim { get; set; }
}