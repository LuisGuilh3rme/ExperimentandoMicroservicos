using System.Data;
using System.Data.SqlClient;
using System.Text;
using AndreTurismoAPIExterna.Models;
using Dapper;

namespace AndreTurismoAPIExterna.Repositories
{
    public class PassagemRepository
    {
        private static string _connection = @"Server=(localdb)\MSSQLLocalDB; Integrated Security=true; AttachDbFileName=C:\Users\adm\AndreTurismoAPIExterna.EnderecoService.Data.mdf";
        private static readonly string _identity = "SELECT CAST(scope_identity() AS INT)";

        public static int InserirPassagem(Passagem passagem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Passagem.INSERT);
            sb.Replace("@Origem", EnderecoRepository.InserirEndereco(passagem.Origem).ToString());
            sb.Replace("@Destino", EnderecoRepository.InserirEndereco(passagem.Destino).ToString());
            sb.Replace("@Cliente", ClienteRepository.InserirCliente(passagem.Cliente).ToString());
            sb.Append(_identity);

            SqlConnection db = new SqlConnection(_connection);
            int id = Convert.ToInt32(db.ExecuteScalar(sb.ToString(), passagem));
            db.Close();

            return id;
        }

        public static Passagem RetornarPassagem(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Id, Id_Origem, Id_Destino, Id_Cliente, Data, Valor FROM Passagem WHERE Id = " + id);

            SqlConnection db = new SqlConnection(_connection);
            db.Open();

            IDataReader dr = db.ExecuteReader(sb.ToString());

            Passagem passagem = new Passagem();

            if (dr.Read())
            {
                passagem.Id = Convert.ToInt32(dr["Id"]);
                passagem.Data = DateTime.Parse(dr["Data"].ToString());
                passagem.Valor = Convert.ToDecimal(dr["Valor"]);

                int idOrigem = Convert.ToInt32(dr["Id_Origem"]);
                int idDestino = Convert.ToInt32(dr["Id_Destino"]);
                int idCliente = Convert.ToInt32(dr["Id_Cliente"]);
                passagem.Origem = EnderecoRepository.RetornarEndereco(idOrigem);
                passagem.Destino = EnderecoRepository.RetornarEndereco(idDestino);
                passagem.Cliente = ClienteRepository.RetornarCliente(idCliente);
            }
            dr.Close();
            db.Close();

            return passagem;
        }
    }
}
