﻿using AndreTurismoAPIExterna.Models.DTO;
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
    public class PacoteController : ControllerBase
    {
        private static PacoteAPIService _pacote;
        private static PassagemAPIService _passagem;
        private static HotelAPIService _hotel;
        private static ClienteAPIService _cliente;

        public PacoteController(PacoteAPIService pacoteAPI, PassagemAPIService passagemAPI, HotelAPIService hotelAPI, ClienteAPIService clienteAPI)
        {
            _pacote = pacoteAPI;
            _passagem = passagemAPI;
            _hotel = hotelAPI;
            _cliente = clienteAPI;
        }


        [HttpGet]
        public ActionResult<List<Pacote>> GetPacote()
        {
            List<Pacote> pacotes = _pacote.Encontrar().Result;
            if (pacotes.Count == 0) return NoContent();
            return pacotes;
        }

        // GET: api/Pacote
        [HttpGet("{id}")]
        public ActionResult<string> GetPacoteById(Guid id)
        {
            Pacote pacote = _pacote.EncontrarPorId(id).Result;
            if (pacote == null) return NotFound();

            Hotel hotel = _hotel.EncontrarPorId(pacote.Hotel).Result;
            if (hotel == null) hotel = new Hotel();

            Passagem passagem = _passagem.EncontrarPorId(pacote.Passagem).Result;
            if (passagem == null) passagem = new Passagem();

            Cliente cliente = _cliente.EncontrarPorId(pacote.Cliente).Result;
            if (cliente == null) cliente = new Cliente();

            return JsonConvert.SerializeObject(new
            {
                Hotel = hotel,
                Passagem = passagem,
                DataCadastro = pacote.DataCadastro,
                Valor = pacote.Valor,
                Cliente = cliente
            }, Formatting.Indented);
        }


        // PUT: api/Pacote/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPacote(Guid id, Pacote pacote)
        {
            if (_pacote.EncontrarPorId(id).Result == null) return NotFound();
            if (_passagem.EncontrarPorId(pacote.Passagem).Result == null) return NotFound();
            if (_hotel.EncontrarPorId(pacote.Hotel).Result == null) return NotFound();
            if (_cliente.EncontrarPorId(pacote.Cliente).Result == null) return NotFound();

            HttpStatusCode code = await _pacote.Atualizar(id, pacote);
            return StatusCode((int)code);
        }

        
        // POST: api/Pacote
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostPacote(Pacote pacote)
        {
            Passagem passagem = _passagem.EncontrarPorId(pacote.Passagem).Result;
            if (passagem == null) return NotFound();

            Hotel hotel = _hotel.EncontrarPorId(pacote.Hotel).Result;
            if (hotel == null) return NotFound();

            Cliente cliente = _cliente.EncontrarPorId(pacote.Cliente).Result;
            if (cliente == null) return NotFound();

            HttpStatusCode code = await _pacote.Enviar(pacote);
            return StatusCode((int)code);
        }
        
        
        // DELETE: api/Pacote/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePacote(int id)
        {
            HttpStatusCode code = await _pacote.Deletar(id);
            return StatusCode((int)code);
        }

    }

}
