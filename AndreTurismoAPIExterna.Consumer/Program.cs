// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using AndreTurismoAPIExterna.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Net;
using AndreTurismoAPIExterna.Consumer.Services;

const string QUEUE_NAME = "Endereco";
EnderecoAPIService _endereco = new EnderecoAPIService();

var factory = new ConnectionFactory() { HostName = "localhost" };

using (var connection = factory.CreateConnection())
{
    using (var channel = connection.CreateModel())
    {
        channel.QueueDeclare(queue: QUEUE_NAME,
                      durable: false,
                      exclusive: false,
                      autoDelete: false,
                      arguments: null);

        while (true)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var returnMessage = Encoding.UTF8.GetString(body);
                var message = JsonConvert.DeserializeObject<Endereco>(returnMessage);

                if (message != null) EnviarEndereco(message);

            };

            channel.BasicConsume(queue: QUEUE_NAME,
                                 autoAck: true,
                                 consumer: consumer);

            Thread.Sleep(2000);
        }
    }
}

async void EnviarEndereco(Endereco endereco)
{
    HttpStatusCode code = await _endereco.Enviar(endereco.CEP, endereco.Numero, endereco);
}