using AndreTurismoAPIExterna.Models.DTO;
using AndreTurismoAPIExterna.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AndreTurismoAPIExterna.Services;
using System.Net;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;

namespace AndreTurismoAPIExterna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassagemController : ControllerBase
    {
        private static PassagemAPIService _passagem;
        private static ClienteAPIService _cliente;
        private static EnderecoAPIService _endereco;

        public PassagemController(PassagemAPIService passagemAPI, ClienteAPIService clienteAPI, EnderecoAPIService enderecoAPI)
        {
            _passagem = passagemAPI;
            _cliente = clienteAPI;
            _endereco = enderecoAPI;
        }


        [HttpGet]
        public ActionResult<List<Passagem>> GetPassagem()
        {
            List<Passagem> passagens = _passagem.Encontrar().Result;
            if (passagens.Count == 0) return NoContent();
            return passagens;
        }

        // GET: api/Passagem
        [HttpGet("{id}")]
        public ActionResult<string> GetPassagemById(Guid id)
        {
            Passagem passagem = _passagem.EncontrarPorId(id).Result;
            if (passagem == null) return NotFound();

            Endereco origem = _endereco.EncontrarPorId(passagem.Origem).Result;
            if (origem == null) origem = new Endereco();

            Endereco destino = _endereco.EncontrarPorId(passagem.Destino).Result;
            if (destino == null) destino = new Endereco();

            Cliente cliente = _cliente.EncontrarPorId(passagem.Cliente).Result;
            if (cliente == null) cliente = new Cliente();

            return JsonConvert.SerializeObject(new
            {
                Origem = origem,
                Destino = destino,
                Cliente = cliente,
                DataViagem = passagem.Data,
                Valor = passagem.Valor
            });
        }


        // PUT: api/Passagem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPassagem(Guid id, Passagem passagem)
        {
            if (_passagem.EncontrarPorId(id) == null) return NotFound();
            if (_endereco.EncontrarPorId(passagem.Origem) == null) return NotFound();
            if (_endereco.EncontrarPorId(passagem.Destino) == null) return NotFound();
            if (_cliente.EncontrarPorId(passagem.Cliente) == null) return NotFound();

            HttpStatusCode code = await _passagem.Atualizar(id, passagem);
            return StatusCode((int)code);
        }


        // POST: api/Passagem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostPassagem(Passagem passagem)
        {
            Endereco endereco;

            endereco = _endereco.EncontrarPorId(passagem.Origem).Result;
            if (endereco == null) return NotFound();

            endereco = _endereco.EncontrarPorId(passagem.Destino).Result;
            if (endereco == null) return NotFound();

            Cliente cliente = _cliente.EncontrarPorId(passagem.Cliente).Result;
            if (cliente == null) return NotFound();

            HttpStatusCode code = await _passagem.Enviar(passagem);
            return StatusCode((int)code);
        }


        // DELETE: api/Passagem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePassagem(Guid id)
        {
            HttpStatusCode code = await _passagem.Deletar(id);
            return StatusCode((int)code);
        }

    }

}
