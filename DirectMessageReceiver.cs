using System;
using System.Linq;
using System.Text;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQConsumer
{
    public class DirectMessageReceiver
    {
        public void Receive()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var queueName = "comilla";
                var exchangeName = "hanif";
                var rooutingKey = "natore";
               // channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);
                channel.ExchangeDeclare(exchangeName, ExchangeType.Direct,true, false);

                channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: rooutingKey);

                Console.WriteLine(" [*] Waiting for "+exchangeName+ ":");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    Console.WriteLine(" [x] {0}", message);
                };
                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);



                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();

            }

        }

    }
}