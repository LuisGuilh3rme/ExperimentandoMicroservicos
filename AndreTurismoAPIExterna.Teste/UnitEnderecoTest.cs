using Microsoft.EntityFrameworkCore;
using AndreTurismoAPIExterna.EnderecoService;
using AndreTurismoAPIExterna.EnderecoService.Data;
using System.Net;
using Microsoft.Extensions.Options;
using AndreTurismoAPIExterna.EnderecoService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AndreTurismoAPIExterna.Teste
{
    public class UnitEnderecoTest
    {

        private DbContextOptions<AndreTurismoAPIExternaEnderecoServiceContext> opcoes;

        private void InicializarBanco()
        {
            // Criar um banco de dados temporário
            opcoes = new DbContextOptionsBuilder<AndreTurismoAPIExternaEnderecoServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            // Inserindo dados no banco
            {
                var contexto = new AndreTurismoAPIExternaEnderecoServiceContext(opcoes);

                contexto.Endereco.Add(new Endereco { Logradouro = "Rua 1", CEP = "123456789", Bairro = "Bairro 1", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade1" } });
                contexto.Endereco.Add(new Endereco { Logradouro = "Rua 2", CEP = "123456789", Bairro = "Bairro 2", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade2" } });
                contexto.Endereco.Add(new Endereco { Logradouro = "Rua 3", CEP = "123456789", Bairro = "Bairro 3", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade3" } });
                contexto.SaveChanges();
            }
        }

        [Fact]
        public void Encontrar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaEnderecoServiceContext(opcoes);
                EnderecoController controlador = new EnderecoController(contexto);
                IEnumerable<Endereco> enderecos = controlador.GetEndereco().Result.Value;
                Assert.Equal(3, enderecos.Count());
            }
        }

        [Fact]
        public void EncontrarPorId()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaEnderecoServiceContext(opcoes);
                EnderecoController controlador = new EnderecoController(contexto);
                Endereco endereco = controlador.GetEndereco(2).Result.Value;
                Assert.Equal("Rua 2", endereco.Logradouro);
            }
        }

        [Fact]
        public void Enviar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaEnderecoServiceContext(opcoes);
                EnderecoController controlador = new EnderecoController(contexto);
                Endereco endereco = new Endereco { CEP = "14945112", Numero = 2, Cidade = new Cidade() };
                Endereco? retorno = controlador.PostEndereco(endereco.CEP, endereco.Numero, endereco).Result.Value;
                Assert.Equal(endereco.CEP, retorno.CEP);
            }
        }

        [Fact]
        public void Atualizar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaEnderecoServiceContext(opcoes);
                EnderecoController controlador = new EnderecoController(contexto);
                Endereco endereco = new Endereco { Id = 1, CEP = "04961990", Numero = 5, Cidade = new Cidade() };
                var retorno = controlador.PutEndereco(endereco.Id, endereco.Numero, endereco).Result.Value;
                Assert.Equal(endereco.CEP, retorno.CEP);
            }
        }
    }
}