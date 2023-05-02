namespace AndreTurismoAPIExterna.Models
{
    public class Cidade
    {
        public static readonly string INSERT = "INSERT INTO Cidade (Nome) VALUES (@Nome);";
        public Guid Id { get; set; }
        public string Nome { get; set; }
    }
}
