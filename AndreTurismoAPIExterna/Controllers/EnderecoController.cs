using AndreTurismoAPIExterna.Models.DTO;
using AndreTurismoAPIExterna.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AndreTurismoAPIExterna.Services;
using System.Net;
using System.Runtime.ConstrainedExecution;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;

namespace AndreTurismoAPIExterna.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {

        private static EnderecoAPIService _endereco;
        private static ConnectionFactory _factory;
        private const string QUEUE_NAME = "Endereco";

        public EnderecoController(ConnectionFactory factory, EnderecoAPIService enderecoAPI)
        {
            _endereco = enderecoAPI;
            _factory = factory;
        }


        [HttpGet]
        public ActionResult<List<Endereco>> GetEndereco()
        {
            List<Endereco> enderecos = _endereco.Encontrar().Result;
            if (enderecos.Count == 0) return NoContent();
            return enderecos;
        }

        // GET: api/Enderecos
        [HttpGet("{id}")]
        public ActionResult<Endereco> GetEnderecoById(Guid id)
        {
            Endereco endereco = _endereco.EncontrarPorId(id).Result;
            if (endereco == null) return NotFound();
            return endereco;
        }


        // PUT: api/Enderecos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}, {numero:int}")]
        public async Task<ActionResult> PutEndereco(Guid id, int numero, Endereco endereco)
        {
            HttpStatusCode code = await _endereco.Atualizar(id, numero, endereco);
            return StatusCode((int)code);
        }

        
        // POST: api/Enderecos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{cep:length(8)}, {numero:int}")]
        public async Task<ActionResult> PostEndereco(string cep, int numero, Endereco endereco)
        {
            endereco.CEP = cep;
            endereco.Numero = numero;

            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    channel.QueueDeclare(
                        queue: QUEUE_NAME,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    var stringfieldMessage = JsonConvert.SerializeObject(endereco);
                    var bytesMessage = Encoding.UTF8.GetBytes(stringfieldMessage);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QUEUE_NAME,
                        basicProperties: null,
                        body: bytesMessage
                        );
                }
            }
            return Accepted();
        }
    

        // DELETE: api/Enderecos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEndereco(Guid id)
        {
            HttpStatusCode code = await _endereco.Deletar(id);
            return StatusCode((int)code);
        }

    }

}
