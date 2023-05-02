using AndreTurismoAPIExterna.PassagemService.Controllers;
using AndreTurismoAPIExterna.PassagemService.Data;

namespace AndreTurismoAPIExterna.Teste
{
    public class UnitPassagemTeste
    {

        private DbContextOptions<AndreTurismoAPIExternaPassagemServiceContext> opcoes;

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

                contexto.Passagem.Add(new Passagem { Origem = Guid.NewGuid(), Destino = Guid.NewGuid(), Cliente = Guid.NewGuid(), Data = DateTime.Now, Valor = 100M});
                contexto.Passagem.Add(new Passagem { Origem = Guid.NewGuid(), Destino = Guid.NewGuid(), Cliente = Guid.NewGuid(), Data = DateTime.Now.AddDays(20), Valor = 100M});
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
                Passagem passagem = controlador.GetPassagem(Guid.NewGuid()).Result.Value;
                Assert.Equal(Guid.NewGuid(), passagem.Cliente);
                Assert.Equal(Guid.NewGuid(), passagem.Cliente);
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
                Passagem passagem = new Passagem { Id = guid, Origem = Guid.NewGuid(), Destino = Guid.NewGuid(), Cliente = Guid.NewGuid(), Data = DateTime.Now, Valor = 200M };

                var retorno = controlador.PutPassagem(guid, passagem).Result.Value;
                Assert.Equal(Guid.NewGuid(), retorno.Origem);
            }
        }

        [Fact]
        public void Deletar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaPassagemServiceContext(opcoes);
                PassagensController controlador = new PassagensController(contexto);

                var retorno = controlador.DeletePassagem(Guid.NewGuid()).Result;
                Assert.IsType<OkResult>(retorno);
            }
        }
    }
}