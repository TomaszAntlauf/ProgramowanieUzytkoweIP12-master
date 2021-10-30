using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRS;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;

namespace ProgramowanieUzytkoweIP12.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CQRSBooksController:ControllerBase
    {
        private readonly CommandBus commandBus;
        private readonly QueryBus queryBus;

        public CQRSBooksController(CommandBus commandBus, QueryBus queryBus)
        {
            this.commandBus = commandBus;
            this.queryBus = queryBus;
        }

        [HttpGet("/Wiele ksiag CQRS")]
        public List<BookDTO> Get([FromQuery] GetBooksQuery query)
        {
            return queryBus.Handle<GetBooksQuery, List<BookDTO>>(query);
        }

        [HttpGet("/Jedna ksiazka CQRS")]
        public BookDTO GetById([FromQuery] GetBookQuery query)
        {
            return queryBus.Handle<GetBookQuery, BookDTO>(query);
        }

        [HttpPost("/Dodaj ksiazke CQRS")]
        public void PostBook([FromBody] AddBookCommand command)
        {
            commandBus.Handle(command);
        }

        [HttpDelete("{id}/Usun ksiazke CQRS")]
        public void Delete(int id)
        {
            commandBus.Handle(new DeleteBookCommand(id));
        }

        [HttpPost("/Dodaj ocene ksiazce CQRS")]
        //public void PostBookRate([FromBody] AddBookRateCommand command)
        public void PostBookRate([FromBody] int id, int rate)
        {
            commandBus.Handle(new AddBookRateCommand(id,rate));
        }

    }
}
