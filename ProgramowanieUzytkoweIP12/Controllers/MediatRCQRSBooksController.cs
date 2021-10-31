using MediatR;
using MediatRCQRS;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramowanieUzytkoweIP12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediatRCQRSBooksController:ControllerBase
    {
        private readonly IMediator mediator;

        public MediatRCQRSBooksController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("/Wiele ksiag MediatR")]
        public Task<List<BookDTO>> Get([FromQuery] GetBooksQueryM query)
        {
            return mediator.Send(query);
        }

    }
}
