using System;
using System.Collections.Generic;
using System.Text;
using EventBus.Infra.EventBus.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace EventBus.Infra.EventBus.Bus
{
    public class RabbitEventBus : IEventBus
    {
        private const string EXCHANGE_NAME = "CommissionExchange";
        private readonly IConnection _connection;

        public RabbitEventBus()
        {
            _connection = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                AutomaticRecoveryEnabled = true,
                TopologyRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(5),
                UseBackgroundThreadsForIO = false
            }.CreateConnection();
        }

        public void Publish<TEvent>(TEvent @event)
            where TEvent : IEvent
        {
            using (var model = _connection.CreateModel())
            {
                string queue = typeof(TEvent).Name;

                var props = model.CreateBasicProperties();
                props.DeliveryMode = 2;
                props.ContentType = "application/json";

                var json = System.Text.Json.JsonSerializer.Serialize(@event);

                var payload = UTF8Encoding.UTF8.GetBytes(json);

                model.ConfirmSelect();

                model.BasicPublish(EXCHANGE_NAME, queue, true, props, payload);

                model.WaitForConfirmsOrDie();
            }
        }

        public void Publish<TEvent>(IEnumerable<TEvent> events)
             where TEvent : IEvent
        {
            using (var model = _connection.CreateModel())
            {
                string queue = typeof(TEvent).Name;

                var props = model.CreateBasicProperties();
                props.DeliveryMode = 2;
                props.ContentType = "application/json";

                model.ConfirmSelect();

                foreach (var m in events)
                {
                    var json = System.Text.Json.JsonSerializer.Serialize(m);

                    var payload = UTF8Encoding.UTF8.GetBytes(json);

                    model.BasicPublish(EXCHANGE_NAME, queue, props, payload);
                }

                model.WaitForConfirmsOrDie();
            }
        }

        public void Subscribe<TEvent>()
             where TEvent : IEvent
        {
            using (var model = _connection.CreateModel())
            {
                var queue = typeof(TEvent).Name;

                var consumer = new EventingBasicConsumer(model);
                consumer.Received += Consumer_Received;
                model.BasicConsume(queue, false, consumer);
            }
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            ((EventingBasicConsumer)sender).Model.BasicAck(e.DeliveryTag, false);
        }

        public void Subscribe<TEvent>(string name)
             where TEvent : IEvent
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<TEvent>()
             where TEvent : IEvent
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<TEvent>(string name)
             where TEvent : IEvent
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void CreateQueue<TEvent>()
             where TEvent : IEvent
        {
            using (var model = _connection.CreateModel())
            {
                model.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Direct, true);
                string queue = typeof(TEvent).Name;
                model.QueueDeclare(queue, true, false, false, null);
                model.QueueBind(queue, EXCHANGE_NAME, queue);
            }
        }
    }
}
