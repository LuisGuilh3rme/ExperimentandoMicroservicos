using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndreTurismoAPIExterna.Models;
using AndreTurismoAPIExterna.PassagemService.Data;
using NuGet.Protocol;

namespace AndreTurismoAPIExterna.PassagemService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassagensController : ControllerBase
    {
        private readonly AndreTurismoAPIExternaPassagemServiceContext _context;

        public PassagensController(AndreTurismoAPIExternaPassagemServiceContext context)
        {
            _context = context;
        }

        // GET: api/Passagens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passagem>>> GetPassagem()
        {
          if (_context.Passagem == null)
          {
              return NotFound();
          }
            return await _context.Passagem.Include(p => p.Origem).ThenInclude(o => o.Cidade)
                .Include(p => p.Destino).ThenInclude(d => d.Cidade)
                .Include(p => p.Cliente).ThenInclude(c => c.Endereco).ThenInclude(e => e.Cidade)
                .ToListAsync();
        }

        // GET: api/Passagens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Passagem>> GetPassagem(int id)
        {
          if (_context.Passagem == null)
          {
              return NotFound();
          }
            Passagem? passagem = await _context.Passagem.Include(p => p.Origem).ThenInclude(o => o.Cidade)
                .Include(p => p.Destino).ThenInclude(d => d.Cidade)
                .Include(p => p.Cliente).ThenInclude(c => c.Endereco).ThenInclude(e => e.Cidade)
                .Where(p => p.Id == id).FirstOrDefaultAsync();

            if (passagem == null)
            {
                return NotFound();
            }

            return passagem;
        }

        // PUT: api/Passagens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassagem(int id, Passagem passagem)
        {
            if (id != passagem.Id)
            {
                return BadRequest();
            }

            _context.Update(passagem.Origem);
            _context.Update(passagem.Destino);
            _context.Update(passagem.Cliente);
            _context.Entry(passagem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PassagemExists(id))
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

        // POST: api/Passagens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Passagem>> PostPassagem(Passagem passagem)
        {
          if (_context.Passagem == null)
          {
              return Problem("Entity set 'AndreTurismoAPIExternaPassagemServiceContext.Passagem'  is null.");
          }
            _context.Passagem.Add(passagem);
            await Console.Out.WriteLineAsync(passagem.ToJson());
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPassagem", new { id = passagem.Id }, passagem);
        }

        // DELETE: api/Passagens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassagem(int id)
        {
            if (_context.Passagem == null)
            {
                return NotFound();
            }
            var passagem = await _context.Passagem.FindAsync(id);
            if (passagem == null)
            {
                return NotFound();
            }

            _context.Passagem.Remove(passagem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PassagemExists(int id)
        {
            return (_context.Passagem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
