using AndreTurismoAPIExterna.Models;
using AndreTurismoAPIExterna.Repositories;

namespace AndreTurismoAPIExterna.Services
{
    public class CidadeService
    {
        public int InserirCidade(Cidade cidade) => CidadeRepository.InserirCidade(cidade);

        public Cidade RetornarCidade(int id) => CidadeRepository.RetornarCidade(id);
    }
}
