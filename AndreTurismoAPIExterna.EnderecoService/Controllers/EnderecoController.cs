﻿using System.Runtime.ConstrainedExecution;
using AndreTurismoAPIExterna.EnderecoService.Data;
using AndreTurismoAPIExterna.EnderecoService.Services;
using AndreTurismoAPIExterna.Models;
using AndreTurismoAPIExterna.Models.DTO;
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
        public async Task<ActionResult<Endereco>> GetEndereco(Guid id)
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
        [HttpPut("{id}, {numero:int}")]
        public async Task<ActionResult<Endereco>> PutEndereco(Guid id, int numero, Endereco endereco)
        {
            Endereco? enderecoExistente = await _context.Endereco.FindAsync(id);
            if (enderecoExistente == null) return NotFound();

            if (endereco.CEP != null)
            {
                EnderecoDTO enderecoDTO = CorreiosService.GetAddress(endereco.CEP).Result;
                if (enderecoDTO == null) return NotFound();

                enderecoExistente.Logradouro = enderecoDTO.Logradouro;
                enderecoExistente.Bairro = enderecoDTO.Bairro;
                enderecoExistente.CEP = enderecoDTO.CEP;
                enderecoExistente.Complemento = enderecoDTO.Complemento;
                enderecoExistente.Cidade = new Cidade()
                {
                    Nome = enderecoDTO.Cidade,
                };
                enderecoExistente.DataCadastro = DateTime.Now;
            }

            enderecoExistente.Numero = numero;
            _context.Entry<Endereco>(enderecoExistente).State = EntityState.Modified;

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

            return endereco;
        }

        // POST: api/Enderecos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{cep:length(8)}, {numero:int}")]
        public async Task<ActionResult<Endereco>> PostEndereco(string cep, int numero, Endereco endereco)
        {
            endereco.Id = Guid.NewGuid();
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

            Cidade cidade = _context.Cidade.Where(c => c.Nome == enderecoDTO.Cidade).FirstOrDefaultAsync().Result;
            if (cidade == null)
            {
                endereco.Cidade.Id = Guid.NewGuid();
                endereco.Cidade.Nome = enderecoDTO.Cidade;
            }
            else endereco.Cidade = cidade;

            _context.Endereco.Add(endereco);
            await _context.SaveChangesAsync();
            return endereco;
        }

        // DELETE: api/Enderecos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndereco(Guid id)
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

            return Ok();
        }

        private bool EnderecoExists(Guid id)
        {
            return (_context.Endereco?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
