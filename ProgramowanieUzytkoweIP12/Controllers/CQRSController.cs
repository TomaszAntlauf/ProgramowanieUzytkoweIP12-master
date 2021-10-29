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
    public class CQRSController:ControllerBase
    {
        private readonly CommandBus commandBus;
        private readonly QueryBus queryBus;

        public CQRSController(CommandBus commandBus, QueryBus queryBus)
        {
            this.commandBus = commandBus;
            this.queryBus = queryBus;
        }

        [HttpGet]
        public List<BookDTO> Get([FromQuery] GetBooksQuery query)
        {
            return queryBus.Handle<GetBooksQuery, List<BookDTO>>(query);
        }

        [HttpPost]
        public void Post([FromBody] AddBookCommand command)
        {
            commandBus.Handle(command);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            commandBus.Handle(new DeleteBookCommand(id));
        }

    }
}
