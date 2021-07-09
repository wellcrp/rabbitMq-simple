using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMq.CrossCutting.Product;
using RabbitMq.Message.Send.RabbitOptions;
using RabbitMq.Message.Send.RabbitSender.Interface.Product;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Message.Send.RabbitSender.Product
{
    public class ProductPublisBus : IProductMessagesSend
    {
        private readonly RabbitConfiguration _config;
        private readonly ConnectionFactory _factory;
        private readonly IConnection _connection;
        private readonly IModel _channel;        
        public ProductPublisBus(IOptions<RabbitConfiguration> options)
        {
            _config = options.Value;

            _factory = new ConnectionFactory
            {
                HostName = options.Value.Hostname,
                UserName = options.Value.UserName,
                Password = options.Value.Password
            };
            _factory.DispatchConsumersAsync = true;

            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();

            //Cria Fila
            _channel.QueueDeclare(
                        queue: _config.Queue,
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
        }

        public void ProductPublish(ProductDto product)
        {
            var json = JsonConvert.SerializeObject(product);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(exchange: "", routingKey: _config.Queue, basicProperties: null, body: body);

        }

        public void ProductConsumer()
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += async (sender, EventArgs) =>
            {
                try
                {
                    var contentArray = EventArgs.Body.ToArray();
                    var contentString = Encoding.UTF8.GetString(contentArray);
                    var _product = JsonConvert.DeserializeObject<ProductDto>(contentString);

                    _channel.BasicAck(EventArgs.DeliveryTag, false);

                    Console.WriteLine("Produto", _product);

                    await Task.Yield();
                }
                catch (Exception ex)
                {
                    //Se caso acontecer alguma coisa, vc faz um Nack do item que recuperou do queue (fila)
                    //retorna o item para fila
                    _channel.BasicNack(EventArgs.DeliveryTag, false, true);
                }
            };
            
            _channel.BasicConsume(queue: _config.Queue, autoAck: true, consumer: consumer);
        }
    }
}
