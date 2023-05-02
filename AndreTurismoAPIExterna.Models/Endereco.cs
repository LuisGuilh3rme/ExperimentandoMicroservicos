﻿using System.ComponentModel.DataAnnotations.Schema;
using AndreTurismoAPIExterna.Models.DTO;

namespace AndreTurismoAPIExterna.Models
{
    public class Endereco
    {
        public Endereco() { }

        public Guid Id { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string Complemento { get; set; }
        public Cidade Cidade { get; set; }
        public DateTime DataCadastro { get; set; }

        public override string ToString()
        {
            return $"\n Rua: {Logradouro} \n Numero: {Numero} \n Bairro: {Bairro} \n CEP: {CEP} \n Complemento: {Complemento} \n Cidade: {Cidade.Nome} \n Data de cadastro: {DataCadastro}";
        }
    }
}
