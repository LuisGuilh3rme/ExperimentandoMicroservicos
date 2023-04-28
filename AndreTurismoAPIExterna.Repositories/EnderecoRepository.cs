using System.Data;
using System.Data.SqlClient;
using System.Text;
using AndreTurismoAPIExterna.Models;
using Dapper;

namespace AndreTurismoAPIExterna.Repositories
{
    public static class EnderecoRepository
    {
        private static string _connection = @"Server=(localdb)\MSSQLLocalDB; Integrated Security=true; AttachDbFileName=C:\Users\adm\AndreTurismoAPIExterna.EnderecoService.Data.mdf";
        private static readonly string _identity = "SELECT CAST(scope_identity() AS INT)";

        public static int InserirEndereco(Endereco endereco)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Endereco.INSERT);
            sb.Replace("@Cidade", CidadeRepository.InserirCidade(endereco.Cidade).ToString());
            sb.Append(_identity);

            SqlConnection db = new SqlConnection(_connection);
            int id = Convert.ToInt32(db.ExecuteScalar(sb.ToString(), endereco));
            db.Close();

            return id;
        }

        public static Endereco RetornarEndereco(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Id, Logradouro, Numero, Bairro, CEP, Complemento, Id_Cidade, Data_Cadastro FROM Endereco WHERE Id = " + id);

            SqlConnection db = new SqlConnection(_connection);
            db.Open();

            IDataReader dr = db.ExecuteReader(sb.ToString());

            Endereco endereco = new Endereco();

            if (dr.Read())
            {
                endereco.Id = Convert.ToInt32(dr["Id"]);
                endereco.Logradouro = Convert.ToString(dr["Logradouro"]);
                endereco.Numero = Convert.ToInt32(dr["Numero"]);
                endereco.Bairro = Convert.ToString(dr["Bairro"]);
                endereco.CEP = Convert.ToString(dr["CEP"]);
                endereco.Complemento = Convert.ToString(dr["Complemento"]);
                endereco.DataCadastro = DateTime.Parse(dr["Data_Cadastro"].ToString());

                int idCidade = Convert.ToInt32(dr["Id_Cidade"]);
                endereco.Cidade = CidadeRepository.RetornarCidade(idCidade);
            }
            dr.Close();
            db.Close();

            return endereco;
        }
    }
}
