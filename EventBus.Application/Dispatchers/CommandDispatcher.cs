using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using EventBus.Application.Commands;

namespace EventBus.Application.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ILifetimeScope _scope;

        public CommandDispatcher(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
                throw new ArgumentNullException(typeof(TCommand).Name, "O comando recebido por parametro não pode ser nulo");

            var handler = _scope.Resolve<ICommandHandler<TCommand>>();

            if (handler == null)
                throw new NullReferenceException("");

            handler.Handler(command);
        }
    }
}
