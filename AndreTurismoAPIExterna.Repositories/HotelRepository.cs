using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AndreTurismoAPIExterna.Models;
using Dapper;

namespace AndreTurismoAPIExterna.Repositories
{
    public static class HotelRepository
    {
        private static string _connection = @"Server=(localdb)\MSSQLLocalDB; Integrated Security=true; AttachDbFileName=C:\Users\adm\AndreTurismoAPIExterna.EnderecoService.Data.mdf";
        private static readonly string _identity = "SELECT CAST(scope_identity() AS INT)";

        public static int InserirHotel(Hotel hotel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Hotel.INSERT);
            sb.Replace("@Endereco", EnderecoRepository.InserirEndereco(hotel.Endereco).ToString());
            sb.Append(_identity);

            SqlConnection db = new SqlConnection(_connection);
            int id = Convert.ToInt32(db.ExecuteScalar(sb.ToString(), hotel));
            db.Close();

            return id;
        }

        public static Hotel RetornarHotel(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Id, Nome, Id_Endereco, Data_Cadastro, Valor FROM Hotel WHERE Id = " + id);

            SqlConnection db = new SqlConnection(_connection);
            db.Open();

            IDataReader dr = db.ExecuteReader(sb.ToString());

            Hotel hotel = new Hotel();

            if (dr.Read())
            {
                hotel.Id = Convert.ToInt32(dr["Id"]);
                hotel.Nome = Convert.ToString(dr["Nome"]);
                hotel.DataCadastro = DateTime.Parse(dr["Data_Cadastro"].ToString());
                hotel.Valor = Convert.ToDecimal(dr["Valor"]);

                int idEndereco = Convert.ToInt32(dr["Id_Endereco"]);
                hotel.Endereco = EnderecoRepository.RetornarEndereco(idEndereco);
            }
            dr.Close();
            db.Close();

            return hotel;
        }

        public static List<Hotel> ListarHotels()
        {
            List<Hotel> list = new List<Hotel>();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Id, Nome, Id_Endereco, Data_Cadastro, Valor FROM Hotel");

            SqlConnection db = new SqlConnection(_connection);
            db.Open();

            IDataReader dr = db.ExecuteReader(sb.ToString());


            while (dr.Read())
            {
                Hotel hotel = new Hotel();

                hotel.Id = Convert.ToInt32(dr["Id"]);
                hotel.Nome = Convert.ToString(dr["Nome"]);
                hotel.DataCadastro = DateTime.Parse(dr["Data_Cadastro"].ToString());
                hotel.Valor = Convert.ToDecimal(dr["Valor"]);

                int idEndereco = Convert.ToInt32(dr["Id_Endereco"]);
                hotel.Endereco = EnderecoRepository.RetornarEndereco(idEndereco);
                list.Add(hotel);
            }
            dr.Close();
            db.Close();

            return list;
        }
    }
}
