using MediatR;
using MediatRCQRS;
using Microsoft.AspNetCore.Mvc;
using Model;
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

        [HttpGet("/Jedna ksiazka MediatR")]
        public Task<BookDTO> GetById([FromQuery] GetBookQueryM query)
        {
            return mediator.Send(query);
        }

        [HttpPost("/Dodaj ksiazke MediatR")]
        public Task<bool> PostBook([FromBody] AddBookCommandM command)
        {
            return mediator.Send(command);
        }

        [HttpDelete("{id}/Usun ksiazke MediatR")]
        public Task<bool> Delete(int id)
        {
            return mediator.Send(new DeleteBookCommandM(id));
        }

        [HttpPost("/Dodaj ocene ksiazce MediatR")]
        public Task<bool> PostBookRate([FromBody] int id, int rate)
        {
            return mediator.Send(new AddBookRateCommandM(id, rate));
        }

    }
}
