using System.Data;
using System.Data.SqlClient;
using System.Text;
using AndreTurismoAPIExterna.Models;
using Dapper;

namespace AndreTurismoAPIExterna.Repositories
{
    public static class CidadeRepository
    {
        private static string _connection = @"Server=(localdb)\MSSQLLocalDB; Integrated Security=true; AttachDbFileName=C:\Users\adm\AndreTurismoAPIExterna.EnderecoService.Data.mdf";
        private static readonly string _identity = "SELECT CAST(scope_identity() AS INT)";

        public static int InserirCidade(Cidade cidade)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Cidade.INSERT);
            sb.Append(_identity);

            SqlConnection db = new SqlConnection(_connection);
            db.Open();
            int id = Convert.ToInt32(db.ExecuteScalar(sb.ToString(), cidade));
            db.Close();

            return id;
        }

        public static Cidade RetornarCidade(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Id, Nome FROM Cidade WHERE Id = " + id);

            SqlConnection db = new SqlConnection(_connection);
            db.Open();

            IDataReader dr = db.ExecuteReader(sb.ToString());

            Cidade cidade = new Cidade();

            if (dr.Read())
            {
                cidade.Id = Convert.ToInt32(dr["Id"]);
                cidade.Nome = Convert.ToString(dr["Nome"]);
            }
            dr.Close();
            db.Close();

            return cidade;
        }
    }
}
