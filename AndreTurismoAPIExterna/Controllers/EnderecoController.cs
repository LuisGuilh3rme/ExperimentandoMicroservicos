using AndreTurismoAPIExterna.Models.DTO;
using AndreTurismoAPIExterna.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AndreTurismoAPIExterna.Services;

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
            return _endereco.GetAddress().Result;
        }

        // GET: api/Enderecos
        [HttpGet("{id}")]
        public ActionResult<Endereco> GetEnderecoById(int id)
        {
            return _endereco.GetAddressById(id).Result;
        }


        // PUT: api/Enderecos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutEndereco(int id, Endereco endereco)
        {
            try
            {
                _endereco.UpdateAddress(id, endereco);
            }
            catch
            {
                return NotFound();
            }
            return NoContent();
        }

        
        // POST: api/Enderecos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{cep:length(8)}, {numero:int}")]
        public async Task<ActionResult> PostEndereco(string cep, int numero, Endereco endereco)
        {
            try
            {
                _endereco.PostAddress(cep, numero, endereco);
            }
            catch
            {
                return BadRequest();
            }
            return NoContent();
        }
        
        /*
        // DELETE: api/Enderecos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            if (_context.Endereco == null)
            {
                return NotFound();
            }
            var endereco = await _context.Endereco.FindAsync(id);
            if (endereco == null)
            {
                return NotFound();
            }

            _context.Endereco.Remove(endereco);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */
    }

}
