namespace AndreTurismoAPIExterna.Models
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public Guid Endereco { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
