using AndreTurismoAPIExterna.Models.DTO;

using Newtonsoft.Json;

namespace AndreTurismoAPIExterna.EnderecoService.Services
{
    public class CorreiosService
    {
        static readonly HttpClient endereco = new HttpClient();
        public static async Task<EnderecoDTO> GetAddress(string cep)
        {
            try
            {
                HttpResponseMessage resposta = await endereco.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
                resposta.EnsureSuccessStatusCode();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<EnderecoDTO>(conteudo);
                return resultado;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
