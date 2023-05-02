using AndreTurismoAPIExterna.ClienteService.Controllers;
using AndreTurismoAPIExterna.ClienteService.Data;

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

                contexto.Cliente.Add(new Cliente { Nome = "Lorem Ipsum", Telefone = "4002-8922", Endereco = Guid.NewGuid(), DataCadastro = DateTime.Now });
                contexto.Cliente.Add(new Cliente { Nome = "Dolor Sit Amet", Telefone = "4002-8922", Endereco = Guid.NewGuid(), DataCadastro = DateTime.Now });
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
                Cliente cliente = controlador.GetCliente(Guid.NewGuid()).Result.Value;
                Assert.Equal("Dolor Sit Amet", cliente.Nome);
                Assert.Equal(Guid.NewGuid(), cliente.Endereco);
            }
        }

        [Fact]
        public void Enviar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaClienteServiceContext(opcoes);
                ClientesController controlador = new ClientesController(contexto);

                Cliente cliente = new Cliente { Nome = "Consectur", Telefone = "4002-8922", Endereco = Guid.NewGuid(), DataCadastro = DateTime.Now };

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

                Guid guid = Guid.NewGuid();
                Cliente cliente = new Cliente { Id = guid, Nome = "Guilherme", Telefone = "4002-8922", Endereco = Guid.NewGuid(), DataCadastro = DateTime.Now };

                var retorno = controlador.PutCliente(guid, cliente).Result.Value;
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

                var retorno = controlador.DeleteCliente(Guid.NewGuid()).Result;
                Assert.IsType<OkResult>(retorno);
            }
        }
    }
}