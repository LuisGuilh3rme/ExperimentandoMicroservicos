using System.Data;
using System.Data.SqlClient;
using System.Text;
using AndreTurismoAPIExterna.Models;
using Dapper;

namespace AndreTurismoAPIExterna.Repositories
{
    public static class ClienteRepository
    {
        private static string _connection = @"Server=(localdb)\MSSQLLocalDB; Integrated Security=true; AttachDbFileName=C:\Users\adm\AndreTurismoAPIExterna.EnderecoService.Data.mdf";
        private static readonly string _identity = "SELECT CAST(scope_identity() AS INT)";

        public static int InserirCliente(Cliente cliente)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Cliente.INSERT);
            sb.Replace("@Endereco", EnderecoRepository.InserirEndereco(cliente.Endereco).ToString());
            sb.Append(_identity);

            SqlConnection db = new SqlConnection(_connection);
            db.Open();
            int id = Convert.ToInt32(db.ExecuteScalar(sb.ToString(), cliente));
            db.Close();

            return id;
        }

        public static Cliente RetornarCliente(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Id, Nome, Telefone, Id_Endereco, Data_Cadastro FROM Cliente WHERE Id = " + id);

            SqlConnection db = new SqlConnection(_connection);
            db.Open();

            IDataReader dr = db.ExecuteReader(sb.ToString());

            Cliente cliente = new Cliente();

            if (dr.Read())
            {
                cliente.Id = Convert.ToInt32(dr["Id"]);
                cliente.Nome = Convert.ToString(dr["Nome"]);
                cliente.Telefone = Convert.ToString(dr["Telefone"]);
                cliente.DataCadastro = DateTime.Parse(dr["Data_Cadastro"].ToString());

                int idEndereco = Convert.ToInt32(dr["Id_Endereco"]);
                cliente.Endereco = EnderecoRepository.RetornarEndereco(idEndereco);
            }
            dr.Close();
            db.Close();

            return cliente;
        }
    }
}
