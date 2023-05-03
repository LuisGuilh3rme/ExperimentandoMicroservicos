using AndreTurismoAPIExterna.EnderecoService.Controllers;
using AndreTurismoAPIExterna.EnderecoService.Data;

namespace AndreTurismoAPIExterna.Teste
{
    public class UnitEnderecoTest
    {

        private DbContextOptions<AndreTurismoAPIExternaEnderecoServiceContext> opcoes;
        private List<Guid> _guids = new List<Guid>();

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

                _guids.Add(Guid.NewGuid());
                _guids.Add(Guid.NewGuid());
                _guids.Add(Guid.NewGuid());

                contexto.Endereco.Add(new Endereco { Id = _guids[0], Logradouro = "Rua 1", CEP = "123456789", Bairro = "Bairro 1", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade1" } });
                contexto.Endereco.Add(new Endereco { Id = _guids[1], Logradouro = "Rua 2", CEP = "123456789", Bairro = "Bairro 2", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade2" } });
                contexto.Endereco.Add(new Endereco { Id = _guids[2], Logradouro = "Rua 3", CEP = "123456789", Bairro = "Bairro 3", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade3" } });
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
                Endereco endereco = controlador.GetEndereco(_guids[1]).Result.Value;
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
                Endereco endereco = new Endereco { Id = _guids[2], CEP = "04961990", Numero = 5, Cidade = new Cidade() };
                var retorno = controlador.PutEndereco(endereco.Id, endereco.Numero, endereco).Result.Value;
                Assert.Equal(endereco.CEP, retorno.CEP);
            }
        }

        [Fact]
        public void Deletar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaEnderecoServiceContext(opcoes);
                EnderecoController controlador = new EnderecoController(contexto);

                var retorno = controlador.DeleteEndereco(_guids[1]).Result;
                Assert.IsType<OkResult>(retorno);
            }
        }
    }
}