using System.Net;
using System.Text;
using AndreTurismoAPIExterna.Models;
using Newtonsoft.Json;

namespace AndreTurismoAPIExterna.Services
{
    public class HotelAPIService
    {
        static readonly HttpClient cliente = new HttpClient();

        public async Task<List<Hotel>> GetHotel()
        {
            try
            {
                HttpResponseMessage resposta = await cliente.GetAsync("https://localhost:5003/api/Hotels");
                resposta.EnsureSuccessStatusCode();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Hotel>>(conteudo);
                return resultado;
            }
            catch (HttpRequestException e)
            {
                return new List<Hotel>();
            }
        }

        public async Task<Hotel> GetHotelById(int id)
        {
            try
            {
                HttpResponseMessage resposta = await cliente.GetAsync("https://localhost:5003/api/Hotels/" + id);
                resposta.EnsureSuccessStatusCode();
                string conteudo = await resposta.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Hotel>(conteudo);
                return resultado;
            }
            catch (HttpRequestException e)
            {
                return null;
            }
        }

        public async Task<HttpStatusCode> UpdateHotel(int id, Hotel hotel)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(hotel), Encoding.UTF8, "application/JSON");
            HttpResponseMessage resposta = await cliente.PutAsync("https://localhost:5003/api/Hotels/" + id, httpContent);
            return resposta.StatusCode;
        }

        public async Task<HttpStatusCode> PostHotel(Hotel hotel)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(hotel), Encoding.UTF8, "application/JSON");
            HttpResponseMessage resposta = await cliente.PostAsync("https://localhost:5003/api/Hotels/", httpContent);
            return resposta.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteHotel(int id)
        {
            HttpResponseMessage resposta = await cliente.DeleteAsync("https://localhost:5003/api/Hotels/" + id);
            return resposta.StatusCode;
        }
    }
}
