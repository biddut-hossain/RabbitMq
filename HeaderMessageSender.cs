﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQConsumer
{
    public class HeaderMessageSender
   {
        public void SendMessage()
            {
                var connectionFactory = new ConnectionFactory()
                {
                    HostName = "localhost"
                };
                var exchangeName = "headerexchange";
                var routingKey = "";

                var connection = connectionFactory.CreateConnection();
                var model = connection.CreateModel();
                var properties = model.CreateBasicProperties();
                properties.Persistent = false;

                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary.Add("format", "pdf");
                dictionary.Add("x-match", "all");
                properties.Headers = dictionary;

                byte[] messagebuffer = Encoding.Default.GetBytes("Message to Headers Exchange 'format=pdf' ");

                model.BasicPublish(exchangeName, routingKey, properties, messagebuffer);

                Console.WriteLine("Message Sent From : headers.exchange ");

                Console.WriteLine("Routing Key : Does not need routing key");

                Console.WriteLine("Message Sent");

            }


        }
    }
