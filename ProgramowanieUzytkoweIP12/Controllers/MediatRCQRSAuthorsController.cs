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
    public class MediatRCQRSAuthorsController : ControllerBase
    {
        private readonly IMediator mediator;

        public MediatRCQRSAuthorsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("/Wielu autorow MediatR")]
        public Task<List<AuthorDTO>> Get([FromQuery] GetAuthorsQueryM query)
        {
            return mediator.Send(query);
        }

        [HttpPost("/Dodaj autora MediatR")]
        public Task<bool> PostAuthor([FromBody] AddAuthorCommandM command)
        {
            return mediator.Send(command);
        }

        [HttpDelete("{id}/Usun autora MediatR")]
        public Task<bool> Delete(int id)
        {
            return mediator.Send(new DeleteAuthorCommandM(id));
        }

        [HttpPost("/Dodaj ocene autorowi MediatR")]
        public Task<bool> PostAuthorRate([FromBody] int id, int rate)
        {
            return mediator.Send(new AddAuthorRateCommandM(id, rate));
        }
    }
}
