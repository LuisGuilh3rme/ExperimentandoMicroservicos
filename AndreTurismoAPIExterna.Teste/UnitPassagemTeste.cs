using AndreTurismoAPIExterna.PassagemService.Controllers;
using AndreTurismoAPIExterna.PassagemService.Data;

namespace AndreTurismoAPIExterna.Teste
{
    public class UnitPassagemTeste
    {

        private DbContextOptions<AndreTurismoAPIExternaPassagemServiceContext> opcoes;
        private List<Guid> _guids = new List<Guid>();

        private void InicializarBanco()
        {
            // Criar um banco de dados temporário
            opcoes = new DbContextOptionsBuilder<AndreTurismoAPIExternaPassagemServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            // Inserindo dados no banco
            {
                var contexto = new AndreTurismoAPIExternaPassagemServiceContext(opcoes);

                _guids.Add(Guid.NewGuid());
                _guids.Add(Guid.NewGuid());
                contexto.Passagem.Add(new Passagem { Id = _guids[0], Origem = Guid.NewGuid(), Destino = Guid.NewGuid(), Cliente = Guid.NewGuid(), Data = DateTime.Now, Valor = 100M});
                contexto.Passagem.Add(new Passagem { Id = _guids[1], Origem = Guid.NewGuid(), Destino = Guid.NewGuid(), Cliente = Guid.NewGuid(), Data = DateTime.Now.AddDays(20), Valor = 100M});
                contexto.SaveChanges();
            }
        }

        [Fact]
        public void Encontrar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaPassagemServiceContext(opcoes);
                PassagensController controlador = new PassagensController(contexto);
                IEnumerable<Passagem> passagens = controlador.GetPassagem().Result.Value;
                Assert.Equal(2, passagens.Count());
            }
        }

        [Fact]
        public void EncontrarPorId()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaPassagemServiceContext(opcoes);
                PassagensController controlador = new PassagensController(contexto);
                Passagem passagem = controlador.GetPassagem(_guids[1]).Result.Value;
                Assert.Equal(100M, passagem.Valor);
            }
        }

        [Fact]
        public void Enviar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaPassagemServiceContext(opcoes);
                PassagensController controlador = new PassagensController(contexto);

                Passagem passagem = new Passagem { Origem = Guid.NewGuid(), Destino = Guid.NewGuid(), Cliente = Guid.NewGuid(), Data = DateTime.Now, Valor = 300M };

                Passagem? retorno = controlador.PostPassagem(passagem).Result.Value;
                Assert.Equal(300M, retorno.Valor);
            }
        }

        [Fact]
        public void Atualizar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaPassagemServiceContext(opcoes);
                PassagensController controlador = new PassagensController(contexto);

                Guid guid = Guid.NewGuid();
                Passagem passagem = new Passagem { Id = _guids[0], Origem = guid, Destino = Guid.NewGuid(), Cliente = Guid.NewGuid(), Data = DateTime.Now, Valor = 200M };

                var retorno = controlador.PutPassagem(_guids[0], passagem).Result.Value;
                Assert.Equal(guid, retorno.Origem);
            }
        }

        [Fact]
        public void Deletar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaPassagemServiceContext(opcoes);
                PassagensController controlador = new PassagensController(contexto);

                var retorno = controlador.DeletePassagem(_guids[1]).Result;
                Assert.IsType<OkResult>(retorno);
            }
        }
    }
}