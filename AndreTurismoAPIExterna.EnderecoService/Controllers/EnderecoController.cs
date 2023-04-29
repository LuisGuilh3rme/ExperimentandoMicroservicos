using AndreTurismoAPIExterna.EnderecoService.Data;
using AndreTurismoAPIExterna.EnderecoService.Services;
using AndreTurismoAPIExterna.Models;
using AndreTurismoAPIExterna.Models.DTO;
using AndreTurismoAPIExterna.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AndreTurismoAPIExterna.EnderecoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly AndreTurismoAPIExternaEnderecoServiceContext _context;

        public EnderecoController(AndreTurismoAPIExternaEnderecoServiceContext context)
        {
            _context = context;
        }

        // GET: api/Enderecos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Endereco>>> GetEndereco()
        {
            if (_context.Endereco == null)
            {
                return NotFound();
            }
            return await _context.Endereco.Include(e => e.Cidade).ToListAsync();
        }

        // GET: api/Enderecos
        [HttpGet("{cep:length(8)}")]
        public ActionResult<EnderecoDTO> GetEnderecoByCep(string cep)
        {
            return CorreiosService.GetAddress(cep).Result;
        }

        // GET: api/Enderecos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Endereco>> GetEndereco(int id)
        {
            if (_context.Endereco == null)
            {
                return NotFound();
            }
            var endereco = await _context.Endereco.Include(e => e.Cidade).Where(e => e.Id == id).FirstOrDefaultAsync();

            if (endereco == null)
            {
                return NotFound();
            }

            return endereco;
        }

        // PUT: api/Enderecos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEndereco(int id, Endereco endereco)
        {
            if (id != endereco.Id)
            {
                return BadRequest();
            }

            _context.Entry(endereco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnderecoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Enderecos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{cep:length(8)}, {numero:int}")]
        public async Task<ActionResult<Endereco>> PostEndereco(string cep, int numero, Endereco endereco)
        {
            endereco.CEP = cep;
            endereco.Numero = numero;

            if (_context.Endereco == null)
            {
                return Problem("Entity set 'AndreTurismoAPIExternaEnderecoServiceContext.Endereco'  is null.");
            }

            EnderecoDTO enderecoDTO = CorreiosService.GetAddress(cep).Result;

            endereco.Bairro = enderecoDTO.Bairro;
            endereco.Logradouro = enderecoDTO.Logradouro;
            endereco.Complemento = enderecoDTO.Complemento;
            endereco.DataCadastro = DateTime.Now;
            endereco.Cidade.Nome = enderecoDTO.Cidade;

            _context.Endereco.Add(endereco);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEndereco", new { id = endereco.Id }, endereco);
        }

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

        private bool EnderecoExists(int id)
        {
            return (_context.Endereco?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
