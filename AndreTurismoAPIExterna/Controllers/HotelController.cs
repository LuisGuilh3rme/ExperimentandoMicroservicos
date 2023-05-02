using AndreTurismoAPIExterna.Models.DTO;
using AndreTurismoAPIExterna.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AndreTurismoAPIExterna.Services;
using System.Net;
using System.Runtime.ConstrainedExecution;

namespace AndreTurismoAPIExterna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private static HotelAPIService _hotel;
        private static EnderecoAPIService _endereco;

        public HotelController(HotelAPIService hotelAPI, EnderecoAPIService enderecoAPI)
        {
            _hotel = hotelAPI;
            _endereco = enderecoAPI;
        }


        [HttpGet]
        public ActionResult<List<Hotel>> GetHotel()
        {
            List<Hotel> hotels = _hotel.Encontrar().Result;
            if (hotels.Count == 0) return NoContent();
            return hotels;
        }

        // GET: api/Enderecos
        [HttpGet("{id}")]
        public ActionResult<Hotel> GetHotelById(Guid id)
        {
            Hotel hotel = _hotel.EncontrarPorId(id).Result;
            if (hotel == null) return NotFound();
            return hotel;
        }


        // PUT: api/Enderecos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutHotel(Guid id, Hotel hotel)
        {
            Endereco endereco = _endereco.EncontrarPorId(hotel.Endereco).Result;
            if (endereco == null) return NotFound();

            HttpStatusCode code = await _hotel.Atualizar(id, hotel);
            return StatusCode((int)code);
        }

        
        // POST: api/Enderecos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostHotel(Hotel hotel)
        {
            Endereco endereco = _endereco.EncontrarPorId(hotel.Endereco).Result;
            if (endereco == null) return NotFound();

            HttpStatusCode code = await _hotel.Enviar(hotel);
            return StatusCode((int)code);
        }
        
        
        // DELETE: api/Enderecos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHotel(Guid id)
        {
            HttpStatusCode code = await _hotel.Deletar(id);
            return StatusCode((int)code);
        }

    }

}
