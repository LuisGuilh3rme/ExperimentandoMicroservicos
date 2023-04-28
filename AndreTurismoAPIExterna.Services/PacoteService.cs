using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoAPIExterna.Models;
using AndreTurismoAPIExterna.Repositories;

namespace AndreTurismoAPIExterna.Services
{
    internal class PacoteService
    {
        public int InserirPacote(Pacote pacote) => PacoteRepository.InserirPacote(pacote);
        public void RemoverPacote(int id)
        {
            PacoteRepository.RemoverPacote(id);
        }
    }
}
