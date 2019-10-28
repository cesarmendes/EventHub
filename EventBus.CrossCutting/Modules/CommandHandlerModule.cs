using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using EventBus.Application.Commands;
using EventBus.Application.Dispatchers;
using EventBus.Infra.EventBus.Events;

namespace EventBus.CrossCutting.Modules
{
    public class CommandHandlerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>().SingleInstance();

            builder.RegisterAssemblyTypes(typeof(ICommand).GetTypeInfo().Assembly)
                   .AsClosedTypesOf(typeof(ICommandHandler<>))
                   .InstancePerLifetimeScope();
        }
    }
}
