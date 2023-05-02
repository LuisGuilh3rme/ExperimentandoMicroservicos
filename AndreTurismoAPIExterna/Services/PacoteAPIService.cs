using System.Net;
using System.Text;
using AndreTurismoAPIExterna.Models;
using Newtonsoft.Json;

namespace AndreTurismoAPIExterna.Services
{
    public class PacoteAPIService
    {
        static readonly HttpClient cliente = new HttpClient();

        public async Task<List<Pacote>> GetPackage()
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

        public async Task<Pacote> GetPackageById(int id)
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

        public async Task<HttpStatusCode> UpdatePackage(int id, Pacote pacote)
        {
            pacote = ClearPackageId(pacote);

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(pacote), Encoding.UTF8, "application/JSON");
            HttpResponseMessage resposta = await cliente.PutAsync("https://localhost:5005/api/Pacotes/" + id, httpContent);
            return resposta.StatusCode;
        }

        public async Task<HttpStatusCode> PostPackage(Pacote pacote)
        {
            pacote = ClearPackageId(pacote);

            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(pacote), Encoding.UTF8, "application/JSON");
            HttpResponseMessage resposta = await cliente.PostAsync("https://localhost:5005/api/Pacotes/", httpContent);
            return resposta.StatusCode;
        }

        public async Task<HttpStatusCode> DeletePackage(int id)
        {
            HttpResponseMessage resposta = await cliente.DeleteAsync("https://localhost:5005/api/Pacotes/" + id);
            return resposta.StatusCode;
        }

        private Pacote ClearPackageId (Pacote pacote)
        {
            pacote.Passagem.Id = 0;
            pacote.Passagem.Origem.Id = 0;
            pacote.Passagem.Origem.Cidade.Id = 0;
            pacote.Passagem.Destino.Id = 0;
            pacote.Passagem.Destino.Cidade.Id = 0;
            pacote.Passagem.Cliente.Id = 0;
            pacote.Passagem.Cliente.Endereco.Id = 0;
            pacote.Passagem.Cliente.Endereco.Cidade.Id = 0;

            pacote.Hotel.Id = 0;
            pacote.Hotel.Endereco.Id = 0;
            pacote.Hotel.Endereco.Cidade.Id = 0;

            pacote.Cliente.Id = 0;
            pacote.Cliente.Endereco.Id = 0;
            pacote.Cliente.Endereco.Cidade.Id = 0;

            return pacote;
        }
    }
}
