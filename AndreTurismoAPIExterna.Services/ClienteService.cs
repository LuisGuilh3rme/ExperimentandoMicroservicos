using AndreTurismoAPIExterna.Models;
using AndreTurismoAPIExterna.Repositories;
using Dapper;

namespace AndreTurismoAPIExterna.Services
{
    public class ClienteService
    {
        public int InserirCliente(Cliente cliente) => ClienteRepository.InserirCliente(cliente);
        public Cliente RetornarCliente(int id) => ClienteRepository.RetornarCliente(id);
    }
}
