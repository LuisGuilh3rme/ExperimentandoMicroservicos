namespace AndreTurismoAPIExterna.Models
{
    public class Passagem
    {
        public Guid Id { get; set; }
        public Guid Origem { get; set; }
        public Guid Destino { get; set; }
        public Guid Cliente { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
    }
}
