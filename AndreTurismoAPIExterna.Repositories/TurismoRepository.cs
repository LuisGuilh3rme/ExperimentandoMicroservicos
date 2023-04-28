using System.Data;
using System.Data.SqlClient;
using System.Text;
using AndreTurismoAPIExterna.Models;
using Dapper;

namespace AndreTurismoAPIExterna.Repositories
{
    public static class TurismoRepository
    {
        private static string _connection = @"Server=(localdb)\MSSQLLocalDB; Integrated Security=true; AttachDbFileName=C:\Users\adm\AndreTurismoAPIExterna.EnderecoService.Data.mdf";
        private static readonly string _identity = "SELECT CAST(scope_identity() AS INT)";

        public static void AtualizarCampo(int id, string tabela, string campo, string valor)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"UPDATE {tabela} SET {campo} = '{valor}' WHERE Id = {id}");

            SqlConnection db = new SqlConnection(_connection);
            db.Open();
            db.Execute(sb.ToString());
            db.Close();
        }

        public static List<Pacote> EncontrarTudo()
        {
            List<Pacote> pacotes = new List<Pacote>();

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Id, Id_Hotel, Id_Passagem, Data_Cadastro, Valor, Id_Cliente FROM Pacote");

            SqlConnection db = new SqlConnection(_connection);
            db.Open();

            IDataReader dr = db.ExecuteReader(sb.ToString());

            while (dr.Read())
            {
                Pacote pacote = new Pacote();

                pacote.Id = Convert.ToInt32(dr["Id"]);
                pacote.DataCadastro = DateTime.Parse(dr["Data_Cadastro"].ToString());
                pacote.Valor = Convert.ToDecimal(dr["Valor"]);

                int idCliente = Convert.ToInt32(dr["Id_Cliente"]);
                int idPassagem = Convert.ToInt32(dr["Id_Passagem"]);
                int idHotel = Convert.ToInt32(dr["Id_Hotel"]);
                pacote.Hotel = HotelRepository.RetornarHotel(idHotel);
                pacote.Passagem = PassagemRepository.RetornarPassagem(idPassagem);
                pacote.Cliente = ClienteRepository.RetornarCliente(idCliente);

                pacotes.Add(pacote);
            }
            dr.Close();
            db.Close();

            return pacotes;
        }
    }
}
