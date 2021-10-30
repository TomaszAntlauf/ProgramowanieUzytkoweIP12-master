using CQRS;
using CQRS.Authors;
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
    public class CQRSAuthorsController
    {
        private readonly CommandBus commandBus;
        private readonly QueryBus queryBus;

        public CQRSAuthorsController(CommandBus commandBus, QueryBus queryBus)
        {
            this.commandBus = commandBus;
            this.queryBus = queryBus;
        }

        [HttpGet("/Wielu autorow CQRS")]
        public List<AuthorDTO> Get([FromQuery] GetAuthorsQuery query)
        {
            return queryBus.Handle<GetAuthorsQuery, List<AuthorDTO>>(query);
        }

        [HttpPost("/Dodaj autora CQRS")]
        public void PostAuthor([FromBody] AddAuthorCommand command)
        {
            commandBus.Handle(command);
        }

        [HttpDelete("{id}/Usun autora CQRS")]
        public void Delete(int id)
        {
            commandBus.Handle(new DeleteAuthorCommand(id));
        }

        [HttpPost("/Dodaj ocene autorowi CQRS")]
        public void PostAuthorRate([FromBody] int id, int rate)
        {
            commandBus.Handle(new AddAuthorRateCommand(id, rate));
        }

    }
}
