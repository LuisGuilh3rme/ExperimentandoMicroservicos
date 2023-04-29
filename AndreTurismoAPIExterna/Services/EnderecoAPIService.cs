using AndreTurismoAPIExterna.Models.DTO;
using AndreTurismoAPIExterna.Models;
using Newtonsoft.Json;
using System.Text.Unicode;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks.Dataflow;

namespace AndreTurismoAPIExterna.Services
{
    public class EnderecoAPIService
    {
        static readonly HttpClient cliente = new HttpClient();

        public async Task<List<Endereco>> GetAddress()
        {
            try
            {
                HttpResponseMessage resposta = await cliente.GetAsync("https://localhost:5001/api/Endereco");
                resposta.EnsureSuccessStatusCode();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Endereco>>(conteudo);
                return resultado;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async Task<Endereco> GetAddressById(int id)
        {
            try
            {
                HttpResponseMessage resposta = await cliente.GetAsync("https://localhost:5001/api/Endereco/" + id);
                resposta.EnsureSuccessStatusCode();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Endereco>(conteudo);
                return resultado;
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
        
        public async void UpdateAddress(int id, Endereco endereco)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(endereco), Encoding.UTF8, "application/JSON");
                HttpResponseMessage resposta = await cliente.PutAsync("https://localhost:5001/api/Endereco/" + id, httpContent);
                resposta.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }

        public async void PostAddress(string cep, int numero, Endereco endereco)
        {
            try
            {
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(endereco), Encoding.UTF8, "application/JSON");
                HttpResponseMessage resposta = await cliente.PostAsync("https://localhost:5001/api/Endereco/" + cep + ", " + numero, httpContent);
                resposta.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                throw;
            }
        }
    }
}
