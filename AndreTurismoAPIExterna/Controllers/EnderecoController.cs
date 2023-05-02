using AndreTurismoAPIExterna.Models.DTO;
using AndreTurismoAPIExterna.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AndreTurismoAPIExterna.Services;
using System.Net;
using System.Runtime.ConstrainedExecution;

namespace AndreTurismoAPIExterna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {

        private static EnderecoAPIService _endereco;

        public EnderecoController(EnderecoAPIService enderecoAPI)
        {
            _endereco = enderecoAPI;
        }


        [HttpGet]
        public ActionResult<List<Endereco>> GetEndereco()
        {
            List<Endereco> enderecos = _endereco.Encontrar().Result;
            if (enderecos.Count == 0) return NoContent();
            return enderecos;
        }

        // GET: api/Enderecos
        [HttpGet("{id}")]
        public ActionResult<Endereco> GetEnderecoById(int id)
        {
            Endereco endereco = _endereco.EncontrarPorId(id).Result;
            if (endereco == null) return NotFound();
            return endereco;
        }


        // PUT: api/Enderecos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}, {numero:int}")]
        public async Task<ActionResult> PutEndereco(int id, int numero, Endereco endereco)
        {
            HttpStatusCode code = await _endereco.Atualizar(id, numero, endereco);
            return StatusCode((int)code);
        }

        
        // POST: api/Enderecos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{cep:length(8)}, {numero:int}")]
        public async Task<ActionResult> PostEndereco(string cep, int numero, Endereco endereco)
        {
            HttpStatusCode code = await _endereco.Enviar(cep, numero, endereco);
            return StatusCode((int)code);
        }
        
        
        // DELETE: api/Enderecos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEndereco(int id)
        {
            HttpStatusCode code = await _endereco.Deletar(id);
            return StatusCode((int)code);
        }

    }

}
