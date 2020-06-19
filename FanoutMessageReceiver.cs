using System;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQConsumer
{
    public class FanoutMessageReceiver

    {
        public void Receive()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var exchangeName = "hanif";
                var routingKey = "natore";
                var queueName = "dhaka";
                //  channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);
                channel.ExchangeDeclare("logs",ExchangeType.Fanout,true,false);
              
                channel.QueueBind(queue: queueName,
                                  exchange: exchangeName,
                                  routingKey: routingKey);

                Console.WriteLine(" [*] Waiting for logs.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    Console.WriteLine(" [x] {0}", message);
                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();

            }

        }

    }
}