using AndreTurismoAPIExterna.Models;
using AndreTurismoAPIExterna.Repositories;

namespace AndreTurismoAPIExterna.Services
{
    public class PassagemService
    {
        public int InserirPassagem(Passagem passagem) => PassagemRepository.InserirPassagem(passagem);
        public Passagem RetornarPassagem(int id) => PassagemRepository.RetornarPassagem(id);
    }
}
