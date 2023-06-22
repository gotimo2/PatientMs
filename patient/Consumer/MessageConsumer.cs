using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Runtime.CompilerServices;

namespace PatientMs.Consumer
{
    public abstract class MessageConsumer
    {

        protected ConnectionFactory connectionFactory;

        public MessageConsumer(ConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        protected void registerQueueListener(string QueueName, EventHandler<BasicDeliverEventArgs> action)
        {
            var channel = connectionFactory.CreateConnection().CreateModel();

            string name = channel.QueueDeclare(queue: QueueName, durable: true, autoDelete: false, exclusive: false).QueueName;

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += action;

            channel.BasicConsume(name, true, consumer);
        }


        public abstract void Initialize();

    }
}
