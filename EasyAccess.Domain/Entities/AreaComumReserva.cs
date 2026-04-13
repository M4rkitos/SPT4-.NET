namespace EasyAccess.Domain.Entities
{
    public class AreaComumReserva
    {
        public int Id { get; set; }
        public int MoradorId { get; set; }
        public string TipoArea { get; set; } 
        public DateTime Data { get; set; }
    }
}