using AndreTurismoAPIExterna.PacoteService.Controllers;
using AndreTurismoAPIExterna.PacoteService.Data;

namespace AndreTurismoAPIExterna.Teste
{
    public class UnitPacoteTeste
    {

        private DbContextOptions<AndreTurismoAPIExternaPacoteServiceContext> opcoes;
        private List<Guid> _guids = new List<Guid>();

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

                _guids.Add(Guid.NewGuid());
                contexto.Pacote.Add(new Pacote { Id = _guids[0], Hotel = Guid.NewGuid(), Passagem = Guid.NewGuid(), DataCadastro = DateTime.Now, Valor = 1000.259M, Cliente = Guid.NewGuid() });
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
                Pacote? pacote = controlador.GetPacote(_guids[0]).Result.Value;

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

                Guid guid = Guid.NewGuid();
                Pacote pacote = new Pacote { Hotel = guid, Passagem = Guid.NewGuid(), DataCadastro = DateTime.Now, Valor = 999.99M, Cliente = Guid.NewGuid() };

                Pacote? retorno = controlador.PostPacote(pacote).Result.Value;
                Assert.Equal(guid, retorno.Hotel);
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
                Pacote pacote = new Pacote { Id = _guids[0], Hotel = Guid.NewGuid(), Passagem = Guid.NewGuid(), DataCadastro = DateTime.Now, Valor = 123.456M, Cliente = guid };

                var retorno = controlador.PutPacote(_guids[0], pacote).Result.Value;
                Assert.Equal(guid, retorno.Cliente);
            }
        }

        [Fact]
        public void Deletar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaPacoteServiceContext(opcoes);
                PacotesController controlador = new PacotesController(contexto);

                var retorno = controlador.DeletePacote(_guids[0]).Result;
                Assert.IsType<OkResult>(retorno);
            }
        }
    }
}