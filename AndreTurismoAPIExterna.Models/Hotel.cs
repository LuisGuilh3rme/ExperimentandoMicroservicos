using System.Reflection.Metadata.Ecma335;

namespace AndreTurismoAPIExterna.Models
{
    public class Hotel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public Endereco Endereco { get; set; }
        public DateTime DataCadastro { get; set; }
        public decimal Valor { get; set; }
    }
}
