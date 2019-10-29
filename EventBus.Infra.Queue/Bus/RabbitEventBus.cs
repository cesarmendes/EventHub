using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using EventBus.Infra.EventBus.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace EventBus.Infra.EventBus.Bus
{
    public class RabbitEventBus : IEventBus
    {
        private const string EXCHANGE_NAME = "CommissionExchange";

        private readonly ILifetimeScope _scope;
        private readonly IConnection _connection;
        private readonly Dictionary<string, Tuple<Type, Type, IModel, IBasicConsumer>> _consumers;

        public RabbitEventBus(ILifetimeScope scope)
        {
            _scope = scope;

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

            _consumers = new Dictionary<string, Tuple<Type, Type, IModel, IBasicConsumer>>();
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

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            //((EventingBasicConsumer)sender).Model.BasicAck(e.DeliveryTag, false);
        }

        public void Subscribe<TEvent, TEventHandler>()
            where TEvent : IEvent
            where TEventHandler : IEventHandler<TEvent>
        {
            var type = typeof(TEvent);
            var queue = type.Name;

            var model = _connection.CreateModel();

            model.QueueDeclare(queue, true, false, false);

            model.QueueBind(queue, EXCHANGE_NAME, queue);

            var consumer = new EventingBasicConsumer(model);
            consumer.Received += Consumer_Received;
            model.BasicConsume(queue, false, consumer);
        }


        public void Unsubscribe<TEvent>()
             where TEvent : IEvent
        {
            throw new NotImplementedException();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }


    }
}
