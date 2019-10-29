using Autofac;
using EventBus.Application.Events;
using EventBus.Infra.EventBus.Bus;
using EventBus.Infra.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.CrossCutting.Modules
{
    public class EventHandlerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //filas
            builder.RegisterType<RabbitEventBus>().As<IEventBus>().SingleInstance();

            builder.RegisterAssemblyTypes(typeof(IIntegrationEvent).Assembly)
                   .AsClosedTypesOf(typeof(IIntegrationEventHandler<>))
                   .InstancePerLifetimeScope();
        }
    }
}
