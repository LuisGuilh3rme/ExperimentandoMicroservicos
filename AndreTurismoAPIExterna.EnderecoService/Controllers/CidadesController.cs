using AndreTurismoAPIExterna.EnderecoService.Data;
using AndreTurismoAPIExterna.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AndreTurismoAPIExterna.EnderecoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadesController : ControllerBase
    {
        private readonly AndreTurismoAPIExternaEnderecoServiceContext _context;

        public CidadesController(AndreTurismoAPIExternaEnderecoServiceContext context)
        {
            _context = context;
        }

        // GET: api/Cidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cidade>>> GetCidade()
        {
          if (_context.Cidade == null)
          {
              return NotFound();
          }
            return await _context.Cidade.ToListAsync();
        }

        // GET: api/Cidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cidade>> GetCidade(int id)
        {
          if (_context.Cidade == null)
          {
              return NotFound();
          }
            var cidade = await _context.Cidade.FindAsync(id);

            if (cidade == null)
            {
                return NotFound();
            }

            return cidade;
        }

        // DELETE: api/Cidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCidade(int id)
        {
            if (_context.Cidade == null)
            {
                return NotFound();
            }
            var cidade = await _context.Cidade.FindAsync(id);
            if (cidade == null)
            {
                return NotFound();
            }

            _context.Cidade.Remove(cidade);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CidadeExists(int id)
        {
            return (_context.Cidade?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
