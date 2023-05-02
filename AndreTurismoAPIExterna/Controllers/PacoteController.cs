using AndreTurismoAPIExterna.Models.DTO;
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
            List<Pacote> pacotes = _pacote.Encontrar().Result;
            if (pacotes.Count == 0) return NoContent();
            return pacotes;
        }

        // GET: api/Pacote
        [HttpGet("{id}")]
        public ActionResult<Pacote> GetPacoteById(int id)
        {
            Pacote pacote = _pacote.EncontrarPorId(id).Result;
            if (pacote == null) return NotFound();
            return pacote;
        }


        // PUT: api/Pacote/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutPacote(int id, Pacote pacote)
        {
            Passagem passagem = _passagem.EncontrarPorId(id).Result;
            if (passagem == null) return NotFound();

            Hotel hotel = _hotel.EncontrarPorId(pacote.Hotel.Id).Result;
            if (hotel == null) return NotFound();

            Cliente cliente = _cliente.EncontrarPorId(pacote.Cliente.Id).Result;
            if (cliente == null) return NotFound();

            HttpStatusCode code = await _pacote.Atualizar(id, pacote);
            return StatusCode((int)code);
        }

        
        // POST: api/Pacote
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostPacote(Pacote pacote)
        {
            Passagem passagem = _passagem.EncontrarPorId(pacote.Passagem.Id).Result;
            if (passagem == null) return NotFound();

            Hotel hotel = _hotel.EncontrarPorId(pacote.Hotel.Id).Result;
            if (hotel == null) return NotFound();

            Cliente cliente = _cliente.EncontrarPorId(pacote.Cliente.Id).Result;
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
