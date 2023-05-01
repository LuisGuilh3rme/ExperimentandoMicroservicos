﻿using AndreTurismoAPIExterna.Models.DTO;
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
            List<Pacote> pacotes = _pacote.GetPackage().Result;
            if (pacotes.Count == 0) return NoContent();
            return pacotes;
        }

        // GET: api/Pacote
        [HttpGet("{id}")]
        public ActionResult<Pacote> GetPacoteById(int id)
        {
            Pacote pacote = _pacote.GetPackageById(id).Result;
            if (pacote == null) return NotFound();
            return pacote;
        }


        // PUT: api/Pacote/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPacote(int id, Pacote pacote)
        {
            Passagem passagem = _passagem.GetTicketById(id).Result;
            if (passagem == null) return NotFound();
            passagem.Origem.Id = 0;
            passagem.Origem.Cidade.Id = 0;
            passagem.Destino.Id = 0;
            passagem.Destino.Cidade.Id = 0;
            passagem.Cliente.Id = 0;
            passagem.Cliente.Endereco.Id = 0;
            passagem.Cliente.Endereco.Cidade.Id = 0;
            pacote.Passagem = passagem;

            Hotel hotel = _hotel.GetHotelById(pacote.Hotel.Id).Result;
            if (hotel == null) return NotFound();
            hotel.Id = 0;
            hotel.Endereco.Id = 0;
            hotel.Endereco.Cidade.Id = 0;
            pacote.Hotel = hotel;

            Cliente cliente = _cliente.GetClientById(pacote.Cliente.Id).Result;
            if (cliente == null) return NotFound();
            cliente.Id = 0;
            cliente.Endereco.Id = 0;
            cliente.Endereco.Cidade.Id = 0;
            pacote.Cliente = cliente;

            HttpStatusCode code = await _pacote.UpdatePackage(id, pacote);
            return StatusCode((int)code);
        }

        
        // POST: api/Pacote
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostPacote(Pacote pacote)
        {
            Passagem passagem = _passagem.GetTicketById(pacote.Passagem.Id).Result;
            if (passagem == null) return NotFound();
            passagem.Id = 0;
            passagem.Origem.Id = 0;
            passagem.Origem.Cidade.Id = 0;
            passagem.Destino.Id = 0;
            passagem.Destino.Cidade.Id = 0;
            passagem.Cliente.Id = 0;
            passagem.Cliente.Endereco.Id = 0;
            passagem.Cliente.Endereco.Cidade.Id = 0;
            pacote.Passagem = passagem;

            Hotel hotel = _hotel.GetHotelById(pacote.Hotel.Id).Result;
            if (hotel == null) return NotFound();
            hotel.Id = 0;
            hotel.Endereco.Id = 0;
            hotel.Endereco.Cidade.Id = 0;
            pacote.Hotel = hotel;

            Cliente cliente = _cliente.GetClientById(pacote.Cliente.Id).Result;
            if (cliente == null) return NotFound();
            cliente.Id = 0;
            cliente.Endereco.Id = 0;
            cliente.Endereco.Cidade.Id = 0;
            pacote.Cliente = cliente;

            HttpStatusCode code = await _pacote.PostPackage(pacote);
            return StatusCode((int)code);
        }
        
        
        // DELETE: api/Pacote/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePacote(int id)
        {
            HttpStatusCode code = await _pacote.DeletePackage(id);
            return StatusCode((int)code);
        }

    }

}