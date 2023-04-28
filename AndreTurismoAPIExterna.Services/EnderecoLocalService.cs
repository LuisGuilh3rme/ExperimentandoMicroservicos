using AndreTurismoAPIExterna.Models;
using AndreTurismoAPIExterna.Repositories;

namespace AndreTurismoAPIExterna.Services
{
    public class EnderecoLocalService
    {
        public int InserirEndereco(Endereco endereco) => EnderecoRepository.InserirEndereco(endereco);
        public Endereco RetornarEndereco(int id) => EnderecoRepository.RetornarEndereco(id);
    }
}
