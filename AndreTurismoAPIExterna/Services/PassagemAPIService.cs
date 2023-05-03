using System.Net;
using System.Text;
using AndreTurismoAPIExterna.Models;
using Newtonsoft.Json;

namespace AndreTurismoAPIExterna.Services
{
    public class PassagemAPIService
    {
        static readonly HttpClient cliente = new HttpClient();

        public async Task<List<Passagem>> Encontrar()
        {
            try
            {
                HttpResponseMessage resposta = await cliente.GetAsync("https://localhost:5004/api/Passagens");
                resposta.EnsureSuccessStatusCode();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Passagem>>(conteudo);
                return resultado;
            }
            catch (HttpRequestException e)
            {
                return new List<Passagem>();
            }
        }

        public async Task<Passagem> EncontrarPorId(Guid id)
        {
            try
            {
                HttpResponseMessage resposta = await cliente.GetAsync("https://localhost:5004/api/Passagens/" + id);
                resposta.EnsureSuccessStatusCode();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Passagem>(conteudo);
                return resultado;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }

        public async Task<HttpStatusCode> Atualizar(Guid id, Passagem passagem)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(passagem), Encoding.UTF8, "application/JSON");
            HttpResponseMessage resposta = await cliente.PutAsync("https://localhost:5004/api/Passagens/" + id, httpContent);
            return resposta.StatusCode;
        }

        public async Task<HttpStatusCode> Enviar(Passagem passagem)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(passagem), Encoding.UTF8, "application/JSON");
            HttpResponseMessage resposta = await cliente.PostAsync("https://localhost:5004/api/Passagens/", httpContent);
            return resposta.StatusCode;
        }

        public async Task<HttpStatusCode> Deletar(Guid id)
        {
            HttpResponseMessage resposta = await cliente.DeleteAsync("https://localhost:5004/api/Passagens/" + id);
            return resposta.StatusCode;
        }
    }
}
