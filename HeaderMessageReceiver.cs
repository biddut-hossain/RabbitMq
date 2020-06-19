using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQConsumer
{
    public class HeaderMessageReceiver

    {
        public void Receive()
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var queueName = "reportpdf";
                var exchangeName = "headerexchange";
                var rooutingKey = "";
                //  channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);


                var model = connection.CreateModel();
                var properties = model.CreateBasicProperties();
                properties.Persistent = false;

                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("format", "pdf");
                dictionary.Add("x-match", "all");

                properties.Headers = dictionary;
                channel.ExchangeDeclare(exchangeName, ExchangeType.Headers, true, false, dictionary);


                channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: rooutingKey);
                Console.WriteLine(" [*] Waiting for " + exchangeName + ":");

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