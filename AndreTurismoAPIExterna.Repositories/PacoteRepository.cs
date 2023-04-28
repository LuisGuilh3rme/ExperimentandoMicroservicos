using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using AndreTurismoAPIExterna.Models;
using Dapper;

namespace AndreTurismoAPIExterna.Repositories
{
    public static class PacoteRepository
    {
        private static string _connection = @"Server=(localdb)\MSSQLLocalDB; Integrated Security=true; AttachDbFileName=C:\Users\adm\AndreTurismoAPIExterna.EnderecoService.Data.mdf";
        private static readonly string _identity = "SELECT CAST(scope_identity() AS INT)";

        public static void RemoverPacote(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"DELETE FROM Pacote WHERE Id = {id}");

            SqlConnection db = new SqlConnection(_connection);
            db.Open();
            db.Execute(sb.ToString());
            db.Close();
        }

        public static int InserirPacote(Pacote pacote)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Pacote.INSERT);
            sb.Replace("@Hotel", HotelRepository.InserirHotel(pacote.Hotel).ToString());
            sb.Replace("@Passagem", PassagemRepository.InserirPassagem(pacote.Passagem).ToString());
            sb.Replace("@Cliente", ClienteRepository.InserirCliente(pacote.Cliente).ToString());
            sb.Append(_identity);

            SqlConnection db = new SqlConnection(_connection);
            db.Open();
            int id = Convert.ToInt32(db.ExecuteScalar(sb.ToString(), pacote));
            db.Close();
            return id;
        }
    }
}
