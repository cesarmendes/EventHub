using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Application.Commands;
using EventBus.Application.Dispatchers;
using Microsoft.AspNetCore.Mvc;

namespace EventBus.Controllers
{
    [ApiController]
    [Route("api/v1/product")]
    public class ProductController : Controller
    {
        private readonly ICommandDispatcher _dispatcher;
        public ProductController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public IActionResult Post([FromBody] SendSummaryCommand command)
        {
            _dispatcher.Send(command);
            return Ok();
        }
    }
}