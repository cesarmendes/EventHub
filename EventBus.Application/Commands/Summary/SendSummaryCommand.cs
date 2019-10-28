using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Application.Commands
{
    public class SendSummaryCommand : ICommand
    {
        public SendSummaryCommand()
        {
        }

        public SendSummaryCommand(int id, DateTime dateStart, DateTime dateEnd)
        {
            Id = id;
            DateStart = DateStart;
            DateEnd = dateEnd;
        }
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
