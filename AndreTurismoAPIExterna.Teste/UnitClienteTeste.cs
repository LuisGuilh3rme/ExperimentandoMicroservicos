using Microsoft.EntityFrameworkCore;
using AndreTurismoAPIExterna.ClienteService.Controllers;
using AndreTurismoAPIExterna.ClienteService.Data;
using AndreTurismoAPIExterna.Models;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoAPIExterna.Teste
{
    public class UnitClienteTeste
    {

        private DbContextOptions<AndreTurismoAPIExternaClienteServiceContext> opcoes;

        private void InicializarBanco()
        {
            // Criar um banco de dados temporário
            opcoes = new DbContextOptionsBuilder<AndreTurismoAPIExternaClienteServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            // Inserindo dados no banco
            {
                var contexto = new AndreTurismoAPIExternaClienteServiceContext(opcoes);
                Endereco endereco = new Endereco { Logradouro = "Rua 1", CEP = "123456789", Bairro = "Bairro 1", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade1" } };

                contexto.Cliente.Add(new Cliente { Nome = "Lorem Ipsum", Telefone = "4002-8922", Endereco = endereco, DataCadastro = DateTime.Now });
                contexto.Cliente.Add(new Cliente { Nome = "Dolor Sit Amet", Telefone = "4002-8922", Endereco = endereco, DataCadastro = DateTime.Now });
                contexto.SaveChanges();
            }
        }

        [Fact]
        public void Encontrar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaClienteServiceContext(opcoes);
                ClientesController controlador = new ClientesController(contexto);
                IEnumerable<Cliente> clientes = controlador.GetCliente().Result.Value;
                Assert.Equal(2, clientes.Count());
            }
        }

        [Fact]
        public void EncontrarPorId()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaClienteServiceContext(opcoes);
                ClientesController controlador = new ClientesController(contexto);
                Cliente cliente = controlador.GetCliente(2).Result.Value;
                Assert.Equal("Dolor Sit Amet", cliente.Nome);
                Assert.Equal("Rua 1", cliente.Endereco.Logradouro);
            }
        }

        [Fact]
        public void Enviar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaClienteServiceContext(opcoes);
                ClientesController controlador = new ClientesController(contexto);

                Endereco endereco = new Endereco { Logradouro = "Rua 2", CEP = "14945112", Numero = 22, Bairro = "Bairro 2", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade2" } };
                Cliente cliente = new Cliente { Nome = "Consectur", Telefone = "4002-8922", Endereco = endereco, DataCadastro = DateTime.Now };

                Cliente? retorno = controlador.PostCliente(cliente).Result.Value;
                Assert.Equal("Consectur", retorno.Nome);
            }
        }

        [Fact]
        public void Atualizar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaClienteServiceContext(opcoes);
                ClientesController controlador = new ClientesController(contexto);

                Endereco endereco = new Endereco { Logradouro = "Rua Guilherme", CEP = "14945112", Numero = 10, Bairro = "Bairro 3", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade3" } };
                Cliente cliente = new Cliente { Id = 1, Nome = "Guilherme", Telefone = "4002-8922", Endereco = endereco, DataCadastro = DateTime.Now };

                var retorno = controlador.PutCliente(1, cliente).Result.Value;
                Assert.Equal("Guilherme", retorno.Nome);
            }
        }

        [Fact]
        public void Deletar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaClienteServiceContext(opcoes);
                ClientesController controlador = new ClientesController(contexto);

                var retorno = controlador.DeleteCliente(1).Result;
                Assert.IsType<OkResult>(retorno);
            }
        }
    }
}