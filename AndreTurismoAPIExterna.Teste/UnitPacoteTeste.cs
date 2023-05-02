using AndreTurismoAPIExterna.PacoteService.Controllers;
using AndreTurismoAPIExterna.PacoteService.Data;

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

                contexto.Pacote.Add(new Pacote { Hotel = Guid.NewGuid(), Passagem = Guid.NewGuid(), DataCadastro = DateTime.Now, Valor = 1000.259M, Cliente = Guid.NewGuid() });
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
                Pacote? pacote = controlador.GetPacote(Guid.NewGuid()).Result.Value;

                Assert.Equal(Guid.NewGuid(), pacote.Hotel);
                Assert.Equal(Guid.NewGuid(), pacote.Passagem);
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

                Pacote pacote = new Pacote { Hotel = Guid.NewGuid(), Passagem = Guid.NewGuid(), DataCadastro = DateTime.Now, Valor = 999.99M, Cliente = Guid.NewGuid() };

                Pacote? retorno = controlador.PostPacote(pacote).Result.Value;
                Assert.Equal(Guid.NewGuid(), pacote.Hotel);
            }
        }

        [Fact]
        public void Atualizar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaPacoteServiceContext(opcoes);
                PacotesController controlador = new PacotesController(contexto);

                Guid guid = Guid.NewGuid();
                Pacote pacote = new Pacote { Id = guid, Hotel = Guid.NewGuid(), Passagem = Guid.NewGuid(), DataCadastro = DateTime.Now, Valor = 123.456M, Cliente = Guid.NewGuid() };


                var retorno = controlador.PutPacote(guid, pacote).Result.Value;
                Assert.Equal(Guid.NewGuid(), retorno.Cliente);
            }
        }

        [Fact]
        public void Deletar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaPacoteServiceContext(opcoes);
                PacotesController controlador = new PacotesController(contexto);

                var retorno = controlador.DeletePacote(Guid.NewGuid()).Result;
                Assert.IsType<OkResult>(retorno);
            }
        }
    }
}