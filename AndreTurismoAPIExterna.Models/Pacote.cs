using System.Text.Json.Serialization;

namespace AndreTurismoAPIExterna.Models
{
    public class Pacote
    {
        public Guid Id { get; set; }
        public Guid Hotel { get; set; }
        public Guid Passagem { get; set; }
        public DateTime DataCadastro { get; set; }
        public decimal Valor { get; set; }
        public Guid Cliente { get; set; }
    }
}
