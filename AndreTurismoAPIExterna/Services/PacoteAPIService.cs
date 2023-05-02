using System.Net;
using System.Text;
using AndreTurismoAPIExterna.Models;
using Newtonsoft.Json;

namespace AndreTurismoAPIExterna.Services
{
    public class PacoteAPIService
    {
        static readonly HttpClient cliente = new HttpClient();

        public async Task<List<Pacote>> Encontrar()
        {
            try
            {
                HttpResponseMessage resposta = await cliente.GetAsync("https://localhost:5005/api/Pacotes");
                resposta.EnsureSuccessStatusCode();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Pacote>>(conteudo);
                return resultado;
            }
            catch (HttpRequestException e)
            {
                return new List<Pacote>();
            }
        }

        public async Task<Pacote> EncontrarPorId(int id)
        {
            try
            {
                HttpResponseMessage resposta = await cliente.GetAsync("https://localhost:5005/api/Pacotes/" + id);
                resposta.EnsureSuccessStatusCode();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Pacote>(conteudo);
                return resultado;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }

        public async Task<HttpStatusCode> Atualizar(Guid id, Pacote pacote)
        {
            pacote = RemoverIds(pacote);

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(pacote), Encoding.UTF8, "application/JSON");
            HttpResponseMessage resposta = await cliente.PutAsync("https://localhost:5005/api/Pacotes/" + id, httpContent);
            return resposta.StatusCode;
        }

        public async Task<HttpStatusCode> Enviar(Pacote pacote)
        {
            pacote = RemoverIds(pacote);

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(pacote), Encoding.UTF8, "application/JSON");
            HttpResponseMessage resposta = await cliente.PostAsync("https://localhost:5005/api/Pacotes/", httpContent);
            return resposta.StatusCode;
        }

        public async Task<HttpStatusCode> Deletar(int id)
        {
            HttpResponseMessage resposta = await cliente.DeleteAsync("https://localhost:5005/api/Pacotes/" + id);
            return resposta.StatusCode;
        }

        private Pacote RemoverIds (Pacote pacote)
        {
            return pacote;
        }
    }
}
