using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Linq;
using RabbitMQConsumer;
class ReceiveLogs
{
    public static void Main()
    {
        FanoutMessageReceiver fanoutMessageReceiver = new FanoutMessageReceiver();
        DirectMessageReceiver directMessageReceiver = new DirectMessageReceiver();
        TopicMessageReceiver topicMessageReceiver = new TopicMessageReceiver();
        HeaderMessageReceiver headerMessageReceiver = new HeaderMessageReceiver();
       

        HeaderMessageSender headerMessageSender = new HeaderMessageSender();
        TopicMessageSender topicMessageSender = new TopicMessageSender();
        FanoutMessageSender fanoutMessageSender = new FanoutMessageSender();
        DirectMessageSender directMessageSender = new DirectMessageSender();
        //   fanoutMessageReceiver.Receive();
        // directMessageReceiver.Receive();
        //topicMessageReceiver.Receive();
        //   headerMessageReceiver.Receive();
        // headerMessageSender.SendMessage();
        // topicMessageSender.SendMessage();
        //fanoutMessageSender.SendMessage();
        directMessageSender.SendMessage();


    }
}