using Autofac;
using EventBus.Infra.EventBus.Bus;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.CrossCutting.Modules
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //filas
            builder.RegisterType<RabbitEventBus>().As<IEventBus>().SingleInstance();
        }
    }
}
