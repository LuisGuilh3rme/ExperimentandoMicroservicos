using System.Text.Json.Serialization;

namespace AndreTurismoAPIExterna.Models
{
    public class Pacote
    {
        public Guid Id { get; set; }
        public Hotel Hotel { get; set; }
        public Passagem Passagem { get; set; }
        public DateTime DataCadastro { get; set; }
        public decimal Valor { get; set; }
        public Cliente Cliente { get; set; }
    }
}
