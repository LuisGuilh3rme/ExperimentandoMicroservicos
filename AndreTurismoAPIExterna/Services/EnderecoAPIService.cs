﻿using AndreTurismoAPIExterna.Models.DTO;
using AndreTurismoAPIExterna.Models;
using Newtonsoft.Json;
using System.Text.Unicode;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks.Dataflow;
using System.Net;

namespace AndreTurismoAPIExterna.Services
{
    public class EnderecoAPIService
    {
        static readonly HttpClient cliente = new HttpClient();

        public async Task<List<Endereco>> Encontrar()
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
                return new List<Endereco>();
            }
        }

        public async Task<Endereco> EncontrarPorId(Guid id)
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
                return null;
            }
        }

        public async Task<HttpStatusCode> Atualizar(Guid id, int numero, Endereco endereco)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(endereco), Encoding.UTF8, "application/JSON");
            HttpResponseMessage resposta = await cliente.PutAsync("https://localhost:5001/api/Endereco/" + id + ", " + numero, httpContent);
            return resposta.StatusCode;
        }

        public async Task<HttpStatusCode> Enviar(string cep, int numero, Endereco endereco)
        {
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(endereco), Encoding.UTF8, "application/JSON");
            HttpResponseMessage resposta = await cliente.PostAsync("https://localhost:5001/api/Endereco/" + cep + ", " + numero, httpContent);
            return resposta.StatusCode;
        }

        public async Task<HttpStatusCode> Deletar(Guid id)
        {
            HttpResponseMessage resposta = await cliente.DeleteAsync("https://localhost:5001/api/Endereco/" + id);
            return resposta.StatusCode;
        }
    }
}
