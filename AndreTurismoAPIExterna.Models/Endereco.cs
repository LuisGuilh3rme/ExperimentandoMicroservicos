using AndreTurismoAPIExterna.Models.DTO;

namespace AndreTurismoAPIExterna.Models
{
    public class Endereco
    {
        public Endereco() { }
        public Endereco(EnderecoDTO enderecoDTO)
        {
            Id = enderecoDTO.Id;
            Logradouro = enderecoDTO.Logradouro;
            Numero = 0;
            Bairro = enderecoDTO.Bairro;
            CEP = enderecoDTO.CEP;
            Complemento = enderecoDTO.Complemento;
            Cidade.Nome = enderecoDTO.Cidade;
            DataCadastro = DateTime.Now;
        }

        public readonly static string INSERT = $"INSERT INTO Endereco (Logradouro, Numero, Bairro, CEP, Complemento, CidadeId, DataCadastro) VALUES (@Logradouro, @Numero, @Bairro, @CEP, @Complemento, @Cidade, @DataCadastro);";
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string Complemento { get; set; }
        public Cidade Cidade { get; set; }
        public DateTime DataCadastro { get; set; }

        public override string ToString()
        {
            return $"\n Rua: {Logradouro} \n Numero: {Numero} \n Bairro: {Bairro} \n CEP: {CEP} \n Complemento: {Complemento} \n Cidade: {Cidade.Nome} \n Data de cadastro: {DataCadastro}";
        }
    }
}
