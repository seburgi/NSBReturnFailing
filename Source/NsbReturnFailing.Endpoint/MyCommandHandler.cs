using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using NsbReturnFailing.Contracts;

namespace NsbReturnFailing.Endpoint
{
    public class MyCommandHandler : IHandleMessages<MyCommand>
    {
        public IBus Bus { get; set; }

        public void Handle(MyCommand message)
        {
            Bus.Return(message.Id);
        }
    }
}
