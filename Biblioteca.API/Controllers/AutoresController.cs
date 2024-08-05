using Biblioteca.API.Application;
using Biblioteca.API.Application.Commands;
using Biblioteca.API.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController(IMediator mediator) : ControllerBase
    {
        // GET: api/<AutoresController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await mediator.Send(new GetAutoresQuery());
            return Ok(result);
        }

        // GET api/<AutoresController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await mediator.Send(new GetAutorByIdQuery(id));

            if (result is null) return NotFound();
            return Ok(result);
        }

        // POST api/<AutoresController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAutorRequestDto requestDto)
        {
            await mediator.Send(new CreateAutorCommand(requestDto.Nome));
            return Created();
        }

        // PUT api/<AutoresController>/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateAutorRequestDto requestDto)
        {
            var result = await mediator.Send(new UpdateAutorCommand(id, requestDto.Nome));

            if (result.StatusCode is ErrorCode.NotFound) return NotFound();

            return NoContent();
        }

        // DELETE api/<AutoresController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteAutorCommand(id));
            return NoContent();
        }
    }
}
