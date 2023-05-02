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
                Endereco origem = new Endereco { Logradouro = "Rua 1", CEP = "123456789", Bairro = "Bairro 1", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade1" } };
                Endereco destino = new Endereco { Logradouro = "Rua 2", CEP = "123456789", Bairro = "Bairro 2", Complemento = "sala 5", Cidade = new Cidade() { Nome = "Cidade2" } };
                Cliente cliente = new Cliente { Nome = "Lorem Ipsum", Telefone = "4002-8922", Endereco = origem, DataCadastro = DateTime.Now };
                Cliente cliente2 = new Cliente { Nome = "Dolor Sit Amet", Telefone = "4002-8922", Endereco = origem, DataCadastro = DateTime.Now };

                contexto.Passagem.Add(new Passagem { Origem = origem, Destino = destino, Cliente = cliente, Data = DateTime.Now, Valor = 100M});
                contexto.Passagem.Add(new Passagem { Origem = destino, Destino = origem, Cliente = cliente2, Data = DateTime.Now.AddDays(20), Valor = 100M});
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
                Assert.Equal("Dolor Sit Amet", passagem.Cliente.Nome);
                Assert.Equal("Rua 1", passagem.Cliente.Endereco.Logradouro);
            }
        }

        [Fact]
        public void Enviar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaPassagemServiceContext(opcoes);
                PassagensController controlador = new PassagensController(contexto);

                Endereco origem = new Endereco { Logradouro = "Rua 20", CEP = "14945112", Numero = 10, Bairro = "Bairro 4", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade1" } };
                Endereco destino = new Endereco { Logradouro = "Rua 1", CEP = "14945112", Numero = 11, Bairro = "Bairro 3", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade2" } };
                Cliente cliente = new Cliente { Nome = "Consectur", Telefone = "4002-8922", Endereco = origem, DataCadastro = DateTime.Now };
                Passagem passagem = new Passagem { Origem = origem, Destino = destino, Cliente = cliente, Data = DateTime.Now, Valor = 300M };

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
                Endereco origem = new Endereco { Logradouro = "Rua 20", CEP = "14945112", Numero = 10, Bairro = "Bairro 4", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade1" } };
                Endereco destino = new Endereco { Logradouro = "Rua 1", CEP = "14945112", Numero = 11, Bairro = "Bairro 3", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade2" } };
                Cliente cliente = new Cliente { Nome = "Sabia", Telefone = "4002-8922", Endereco = origem, DataCadastro = DateTime.Now };
                Passagem passagem = new Passagem { Id = guid, Origem = destino, Destino = origem, Cliente = cliente, Data = DateTime.Now, Valor = 200M };

                var retorno = controlador.PutPassagem(guid, passagem).Result.Value;
                Assert.Equal("Rua 1", retorno.Origem.Logradouro);
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