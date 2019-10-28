using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Application.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace EventBus.Controllers
{
    [ApiController]
    [Route("api/v1/message")]
    public class MessageController : Controller
    {
        private readonly ICommandDispatcher _dispatcher;
        public MessageController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        //[HttpPost]
        //public IActionResult Post([FromBody] SendMessageCommand command) 
        //{
        //    _dispatcher.Send(command);
        //    return Ok();
        //}
    }
}