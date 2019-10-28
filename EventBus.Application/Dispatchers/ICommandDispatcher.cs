using EventBus.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Application.Dispatchers
{
    public interface ICommandDispatcher
    {
        void Send<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
