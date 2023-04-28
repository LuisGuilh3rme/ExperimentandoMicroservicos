using AndreTurismoAPIExterna.Models;
using AndreTurismoAPIExterna.Repositories;

namespace AndreTurismoAPIExterna.Services
{
    public class TurismoService
    {
        public void AtualizarCampo (int id, string tabela, string campo, string valor)
        {
            TurismoRepository.AtualizarCampo(id, tabela, campo, valor);
        }

        public List<Pacote> EncontrarTudo() => TurismoRepository.EncontrarTudo();
    }
}
