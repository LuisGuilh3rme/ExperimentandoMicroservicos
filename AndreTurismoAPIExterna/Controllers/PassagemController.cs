using AndreTurismoAPIExterna.Models.DTO;
using AndreTurismoAPIExterna.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AndreTurismoAPIExterna.Services;
using System.Net;
using System.Runtime.ConstrainedExecution;
using Microsoft.AspNetCore.Http.Features;

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
        public ActionResult<Passagem> GetPassagemById(int id)
        {
            Passagem passagem = _passagem.EncontrarPorId(id).Result;
            if (passagem == null) return NotFound();
            return passagem;
        }


        // PUT: api/Enderecos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPassagem(int id, Passagem passagem)
        {
            Endereco endereco;

            endereco = _endereco.GetAddressById(passagem.Origem.Id).Result;
            if (endereco == null) return NotFound();
            
            endereco = _endereco.GetAddressById(passagem.Destino.Id).Result;
            if (endereco == null) return NotFound();

            Cliente cliente = _cliente.GetClientById(passagem.Cliente.Id).Result;
            if (cliente == null) return NotFound();

            HttpStatusCode code = await _passagem.Atualizar(id, passagem);
            return StatusCode((int)code);
        }

        
        // POST: api/Enderecos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostPassagem(Passagem passagem)
        {
            Endereco endereco;

            endereco = _endereco.GetAddressById(passagem.Origem.Id).Result;
            if (endereco == null) return NotFound();

            endereco = _endereco.GetAddressById(passagem.Destino.Id).Result;
            if (endereco == null) return NotFound();

            Cliente cliente = _cliente.GetClientById(passagem.Cliente.Id).Result;
            if (cliente == null) return NotFound();

            HttpStatusCode code = await _passagem.Enviar(passagem);
            return StatusCode((int)code);
        }
        
        
        // DELETE: api/Enderecos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePassagem(int id)
        {
            HttpStatusCode code = await _passagem.Deletar(id);
            return StatusCode((int)code);
        }

    }

}
