using AndreTurismoAPIExterna.HotelService.Controllers;
using AndreTurismoAPIExterna.HotelService.Data;

namespace AndreTurismoAPIExterna.Teste
{
    public class UnitHotelTeste
    {

        private DbContextOptions<AndreTurismoAPIExternaHotelServiceContext> opcoes;

        private void InicializarBanco()
        {
            // Criar um banco de dados temporário
            opcoes = new DbContextOptionsBuilder<AndreTurismoAPIExternaHotelServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            // Inserindo dados no banco
            {
                var contexto = new AndreTurismoAPIExternaHotelServiceContext(opcoes);
                Endereco endereco = new Endereco { Logradouro = "Rua 1", CEP = "123456789", Bairro = "Bairro 1", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade1" } };

                contexto.Hotel.Add(new Hotel { Nome = "Hotel 1", Endereco = endereco, DataCadastro = DateTime.Now, Valor = 100});
                contexto.Hotel.Add(new Hotel { Nome = "Hotel 2", Endereco = endereco, DataCadastro = DateTime.Now, Valor = 200});
                contexto.SaveChanges();
            }
        }

        [Fact]
        public void Encontrar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaHotelServiceContext(opcoes);
                HotelsController controlador = new HotelsController(contexto);
                IEnumerable<Hotel> hoteis = controlador.GetHotel().Result.Value;
                Assert.Equal(2, hoteis.Count());
            }
        }

        [Fact]
        public void EncontrarPorId()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaHotelServiceContext(opcoes);
                HotelsController controlador = new HotelsController(contexto);
                Hotel hotel = controlador.GetHotel(2).Result.Value;
                Assert.Equal("Hotel 2", hotel.Nome);
                Assert.Equal("Rua 1", hotel.Endereco.Logradouro);
            }
        }

        [Fact]
        public void Enviar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaHotelServiceContext(opcoes);
                HotelsController controlador = new HotelsController(contexto);

                Endereco endereco = new Endereco { Logradouro = "Rua 2", CEP = "14945112", Numero = 22, Bairro = "Bairro 2", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade2" } };
                Hotel hotel = new Hotel { Nome = "Hotel 3", Endereco = endereco, DataCadastro = DateTime.Now, Valor = 1.99M };

                Hotel? retorno = controlador.PostHotel(hotel).Result.Value;
                Assert.Equal(1.99M, retorno.Valor);
            }
        }

        [Fact]
        public void Atualizar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaHotelServiceContext(opcoes);
                HotelsController controlador = new HotelsController(contexto);

                Endereco endereco = new Endereco { Logradouro = "Rua Guilherme", CEP = "14945112", Numero = 10, Bairro = "Bairro 3", Complemento = "null", Cidade = new Cidade() { Nome = "Cidade3" } };
                Hotel hotel = new Hotel { Id = 1, Nome = "Hotel Guilherme", Endereco = endereco, DataCadastro = DateTime.Now, Valor = 20M };

                var retorno = controlador.PutHotel(1, hotel).Result.Value;
                Assert.Equal("Hotel Guilherme", retorno.Nome);
            }
        }

        [Fact]
        public void Deletar()
        {
            InicializarBanco();

            {
                var contexto = new AndreTurismoAPIExternaHotelServiceContext(opcoes);
                HotelsController controlador = new HotelsController(contexto);

                var retorno = controlador.DeleteHotel(1).Result;
                Assert.IsType<OkResult>(retorno);
            }
        }
    }
}