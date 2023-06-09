﻿using AndreTurismoAPIExterna.Models.DTO;
using AndreTurismoAPIExterna.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AndreTurismoAPIExterna.Services;
using System.Net;
using System.Runtime.ConstrainedExecution;
using Newtonsoft.Json;

namespace AndreTurismoAPIExterna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private static ClienteAPIService _cliente;
        private static EnderecoAPIService _endereco;

        public ClienteController(ClienteAPIService clienteAPI, EnderecoAPIService enderecoAPI)
        {
            _cliente = clienteAPI;
            _endereco = enderecoAPI;
        }


        [HttpGet]
        public ActionResult<List<Cliente>> GetCliente()
        {
            List<Cliente> clientes = _cliente.Encontrar().Result;
            if (clientes.Count == 0) return NoContent();
            return clientes;
        }

        // GET: api/Enderecos
        [HttpGet("{id}")]
        public ActionResult<string> GetClienteById(Guid id)
        {
            Cliente cliente = _cliente.EncontrarPorId(id).Result;
            if (cliente == null) return NotFound();

            Endereco endereco = _endereco.EncontrarPorId(cliente.Endereco).Result; 
            if (endereco == null) return NotFound();


            return JsonConvert.SerializeObject(new {
                Nome = cliente.Nome,
                Telefone = cliente.Telefone,
                Endereco = endereco,
                DataCadastro = cliente.DataCadastro
            }, Formatting.Indented);
        }


        // PUT: api/Enderecos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCliente(Guid id, Cliente cliente)
        {
            if (_cliente.EncontrarPorId(id).Result == null) return NotFound();
            if (_endereco.EncontrarPorId(cliente.Endereco).Result == null) return NotFound();

            HttpStatusCode code = await _cliente.Atualizar(id, cliente);
            return StatusCode((int)code);
        }

        
        // POST: api/Enderecos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostCliente(Cliente cliente)
        {
            Endereco endereco = _endereco.EncontrarPorId(cliente.Endereco).Result;
            if (endereco == null) return NotFound();

            HttpStatusCode code = await _cliente.Enviar(cliente);
            return StatusCode((int)code);
        }
        
        
        // DELETE: api/Enderecos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCliente(Guid id)
        {
            HttpStatusCode code = await _cliente.Deletar(id);
            return StatusCode((int)code);
        }

    }

}
