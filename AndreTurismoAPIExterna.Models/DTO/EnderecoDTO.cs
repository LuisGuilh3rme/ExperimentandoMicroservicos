using Newtonsoft;
using Newtonsoft.Json;

namespace AndreTurismoAPIExterna.Models.DTO
{
    public class EnderecoDTO
    {
        public int Id { get; set; }
        [JsonProperty("cep")]
        public string CEP { get; set; }
        [JsonProperty("bairro")]
        public string Bairro { get; set; }
        [JsonProperty("localidade")]
        public string Cidade { get; set; }
        [JsonProperty("uf")]
        public string Estado { get; set; }
        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }
        [JsonProperty("complemento")]
        public string Complemento { get; set; }
    }
}
