using Biblioteca.API.Application;
using Biblioteca.API.Application.Commands;
using Biblioteca.API.Application.Queries;
using Biblioteca.API.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssuntosController(IMediator mediator) : ApiController
    {
        // GET: api/<AssuntosController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await mediator.Send(new GetAssuntosQuery());
            return Ok(result);
        }

        // GET api/<AssuntosController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await mediator.Send(new GetAssuntoByIdQuery(id));

            if (result is null) return NotFound();

            return Ok(result);
        }

        // POST api/<AssuntosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAssuntoRequestDto requestDto)
        {
            await mediator.Send(new CreateAssuntoCommand(requestDto.Descricao));
            return Created();
        }

        // PUT api/<AssuntosController>/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateAssuntoRequestDto requestDto)
        {
            var result = await mediator.Send(new UpdateAssuntoCommand(id, requestDto.Descricao));
            return HandleResponse(result, NoContent());
        }

        // DELETE api/<AssuntosController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await mediator.Send(new DeleteAssuntoCommand(id));
            return HandleResponse(result, NoContent());
        }
    }
}
