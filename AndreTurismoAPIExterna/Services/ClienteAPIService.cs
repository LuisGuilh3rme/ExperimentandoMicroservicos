using System.Net;
using System.Text;
using AndreTurismoAPIExterna.Models;
using Newtonsoft.Json;

namespace AndreTurismoAPIExterna.Services
{
    public class ClienteAPIService
    {
        static readonly HttpClient cliente = new HttpClient();

        public async Task<List<Cliente>> GetClient()
        {
            try
            {
                HttpResponseMessage resposta = await cliente.GetAsync("https://localhost:5002/api/Clientes");
                resposta.EnsureSuccessStatusCode();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Cliente>>(conteudo);
                return resultado;
            }
            catch (HttpRequestException e)
            {
                return new List<Cliente>();
            }
        }

        public async Task<Cliente> GetClientById(int id)
        {
            try
            {
                HttpResponseMessage resposta = await cliente.GetAsync("https://localhost:5002/api/Clientes/" + id);
                resposta.EnsureSuccessStatusCode();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Cliente>(conteudo);
                return resultado;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }

        public async Task<HttpStatusCode> UpdateClient(int id, Cliente c)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/JSON");
            HttpResponseMessage resposta = await cliente.PutAsync("https://localhost:5002/api/Clientes/" + id, httpContent);
            return resposta.StatusCode;
        }

        public async Task<HttpStatusCode> PostClient(Cliente c)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(c), Encoding.UTF8, "application/JSON");
            HttpResponseMessage resposta = await cliente.PostAsync("https://localhost:5002/api/Clientes/", httpContent);
            return resposta.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteClient(int id)
        {
            HttpResponseMessage resposta = await cliente.DeleteAsync("https://localhost:5002/api/Clientes/" + id);
            return resposta.StatusCode;
        }
    }
}
