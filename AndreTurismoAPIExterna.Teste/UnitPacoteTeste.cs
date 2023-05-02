using Microsoft.EntityFrameworkCore;
using AndreTurismoAPIExterna.PacoteService.Controllers;
using AndreTurismoAPIExterna.PacoteService.Data;
using Microsoft.AspNetCore.Mvc;

namespace AndreTurismoAPIExterna.Teste
{
    public class UnitPacoteTeste
    {

        private DbContextOptions<AndreTurismoAPIExternaPacoteServiceContext> opcoes;

        private void InicializarBanco()
        {
            // Criar um banco de dados temporário
            opcoes = new DbContextOptionsBuilder<AndreTurismoAPIExternaPacoteServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            // Inserindo dados no banco
            {
                var contexto = new AndreTurismoAPIExternaPacoteServiceContext(opcoes);
                Endereco origem = new Endereco { Logradouro = "Rua 1", CEP = "123456789", Bairro = "Bairro 1", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade1" } };
                Endereco destino = new Endereco { Logradouro = "Rua 2", CEP = "123456789", Bairro = "Bairro 2", Complemento = "sala 5", Cidade = new Cidade() { Nome = "Cidade2" } };
                Cliente cliente = new Cliente { Nome = "Lorem Ipsum", Telefone = "4002-8922", Endereco = origem, DataCadastro = DateTime.Now };
                Passagem passagem = new Passagem { Origem = origem, Destino = destino, Cliente = cliente, Data = DateTime.Now, Valor = 100M };
                Hotel hotel = new Hotel { Nome = "Hotel 1", Endereco = origem, DataCadastro = DateTime.Now, Valor = 100 };

                contexto.Pacote.Add(new Pacote { Hotel = hotel, Passagem = passagem, DataCadastro = DateTime.Now, Valor = 1000.259M, Cliente = cliente});
                contexto.SaveChanges();
            }
        }

        [Fact]
        public void Encontrar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaPacoteServiceContext(opcoes);
                PacotesController controlador = new PacotesController(contexto);
                IEnumerable<Pacote> passagens = controlador.GetPacote().Result.Value;
                Assert.Equal(1, passagens.Count());
            }
        }

        [Fact]
        public void EncontrarPorId()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaPacoteServiceContext(opcoes);
                PacotesController controlador = new PacotesController(contexto);
                Pacote? pacote = controlador.GetPacote(1).Result.Value;

                Assert.Equal("Hotel 1", pacote.Hotel.Nome);
                Assert.Equal("Rua 1", pacote.Passagem.Origem.Logradouro);
                Assert.Equal(1000.259M, pacote.Valor);
            }
        }

        [Fact]
        public void Enviar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaPacoteServiceContext(opcoes);
                PacotesController controlador = new PacotesController(contexto);

                Endereco origem = new Endereco { Logradouro = "Rua 20", CEP = "14945112", Numero = 10, Bairro = "Bairro 4", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade1" } };
                Endereco destino = new Endereco { Logradouro = "Rua 1", CEP = "14945112", Numero = 11, Bairro = "Bairro 3", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade2" } };
                Cliente cliente = new Cliente { Nome = "Consectur", Telefone = "4002-8922", Endereco = origem, DataCadastro = DateTime.Now };
                Passagem passagem = new Passagem { Origem = origem, Destino = destino, Cliente = cliente, Data = DateTime.Now, Valor = 300M };
                Hotel hotel = new Hotel { Nome = "Hotel 20", Endereco = origem, DataCadastro = DateTime.Now, Valor = 100 };
                Pacote pacote = new Pacote { Hotel = hotel, Passagem = passagem, DataCadastro = DateTime.Now, Valor = 999.99M, Cliente = cliente };

                Pacote? retorno = controlador.PostPacote(pacote).Result.Value;
                Assert.Equal("Hotel 20", pacote.Hotel.Nome);
            }
        }

        [Fact]
        public void Atualizar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaPacoteServiceContext(opcoes);
                PacotesController controlador = new PacotesController(contexto);

                Endereco origem = new Endereco { Logradouro = "Rua 8", CEP = "14945112", Numero = 10, Bairro = "Bairro 4", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade1" } };
                Endereco destino = new Endereco { Logradouro = "Rua 9", CEP = "14945112", Numero = 11, Bairro = "Bairro 3", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade2" } };
                Cliente cliente = new Cliente { Nome = "Marquinhos", Telefone = "4002-8922", Endereco = origem, DataCadastro = DateTime.Now };
                Passagem passagem = new Passagem { Origem = destino, Destino = origem, Cliente = cliente, Data = DateTime.Now, Valor = 200M };
                Hotel hotel = new Hotel { Nome = "Hotel Lorem Ipsum", Endereco = origem, DataCadastro = DateTime.Now, Valor = 100 };
                Pacote pacote = new Pacote { Id = 1, Hotel = hotel, Passagem = passagem, DataCadastro = DateTime.Now, Valor = 123.456M, Cliente = cliente };


                var retorno = controlador.PutPacote(1, pacote).Result.Value;
                Assert.Equal("Marquinhos", retorno.Cliente.Nome);
            }
        }

        [Fact]
        public void Deletar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaPacoteServiceContext(opcoes);
                PacotesController controlador = new PacotesController(contexto);

                var retorno = controlador.DeletePacote(1).Result;
                Assert.IsType<OkResult>(retorno);
            }
        }
    }
}