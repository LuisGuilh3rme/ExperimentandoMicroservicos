using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismoAPIExterna.Models;
using AndreTurismoAPIExterna.PacoteService.Data;
using NuGet.Protocol;

namespace AndreTurismoAPIExterna.PacoteService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacotesController : ControllerBase
    {
        private readonly AndreTurismoAPIExternaPacoteServiceContext _context;

        public PacotesController(AndreTurismoAPIExternaPacoteServiceContext context)
        {
            _context = context;
        }

        // GET: api/Pacotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pacote>>> GetPacote()
        {
          if (_context.Pacote == null)
          {
              return NotFound();
          }
            return await _context.Pacote
                .Include(p => p.Hotel).ThenInclude(h => h.Endereco).ThenInclude(e => e.Cidade)
                .Include(p => p.Passagem).ThenInclude(p => p.Origem).ThenInclude(e => e.Cidade)
                .Include(p => p.Passagem).ThenInclude(p => p.Destino).ThenInclude(e => e.Cidade)
                .Include(p => p.Passagem).ThenInclude(p => p.Cliente).ThenInclude(c => c.Endereco).ThenInclude(e => e.Cidade)
                .Include(p => p.Cliente).ThenInclude(c => c.Endereco).ThenInclude(e => e.Cidade)
                .ToListAsync();
        }

        // GET: api/Pacotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pacote>> GetPacote(int id)
        {
          if (_context.Pacote == null)
          {
              return NotFound();
          }
            var pacote = await _context.Pacote
                .Include(p => p.Hotel).ThenInclude(h => h.Endereco).ThenInclude(e => e.Cidade)
                .Include(p => p.Passagem).ThenInclude(p => p.Origem).ThenInclude(e => e.Cidade)
                .Include(p => p.Passagem).ThenInclude(p => p.Destino).ThenInclude(e => e.Cidade)
                .Include(p => p.Passagem).ThenInclude(p => p.Cliente).ThenInclude(c => c.Endereco).ThenInclude(e => e.Cidade)
                .Include(p => p.Cliente).ThenInclude(c => c.Endereco).ThenInclude(e => e.Cidade)
                .Where(p => p.Id == id).FirstOrDefaultAsync();

            if (pacote == null)
            {
                return NotFound();
            }

            return pacote;
        }

        // PUT: api/Pacotes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Pacote>> PutPacote(int id, Pacote pacote)
        {
            if (id != pacote.Id)
            {
                return BadRequest();
            }

            _context.Update(pacote.Hotel);
            await _context.SaveChangesAsync();

            _context.Update(pacote.Passagem);
            await _context.SaveChangesAsync();

            _context.Update(pacote.Cliente);
            await _context.SaveChangesAsync();

            _context.Entry(pacote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return pacote;
        }

        // POST: api/Pacotes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pacote>> PostPacote(Pacote pacote)
        {
          if (_context.Pacote == null)
          {
              return Problem("Entity set 'AndreTurismoAPIExternaPacoteServiceContext.Pacote'  is null.");
          }
            _context.Pacote.Add(pacote);
            await _context.SaveChangesAsync();

            return pacote;
        }

        // DELETE: api/Pacotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePacote(int id)
        {
            if (_context.Pacote == null)
            {
                return NotFound();
            }
            var pacote = await _context.Pacote.FindAsync(id);
            if (pacote == null)
            {
                return NotFound();
            }

            _context.Pacote.Remove(pacote);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool PacoteExists(int id)
        {
            return (_context.Pacote?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
