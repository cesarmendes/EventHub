using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Application.Commands
{
    public interface ICommandHandler<in TCommand>
        where TCommand : ICommand
    {
        void Handler(TCommand command);
    }
}
