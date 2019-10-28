using EventBus.Application.Events.Summary;
using EventBus.Domain.Entities;
using EventBus.Infra.EventBus.Bus;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Application.Commands
{
    public class SendSummaryCommandHandler : ICommandHandler<SendSummaryCommand>
    {
        private readonly IEventBus _bus;

        public SendSummaryCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }

        public void Handler(SendSummaryCommand command)
        {
            //Obtém sumarizado as ordems do produto via repositorio
            //SummaryRepository.GetSummary(command.DateStart, command.DateEnd);
            
            //Resultado mocado
            var summary = new Summary() { ProductGuid = Guid.NewGuid(), Quantity = 100, Total = 10000 };

            _bus.Publish(new SummaryIntegrationEvent(command.Id, summary.Quantity, summary.Total));
        }
    }
}
