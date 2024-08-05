using Biblioteca.API.Application;
using Biblioteca.API.Application.Commands;
using Biblioteca.API.Application.Queries;
using Biblioteca.API.Application.Queries.Autor;
using Biblioteca.API.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController(IMediator mediator) : ControllerBase
    {
        // GET: api/<LivrosController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await mediator.Send(new GetLivrosQuery());
            return Ok(result);
        }

        // GET api/<LivrosController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await mediator.Send(new GetLivroByIdQuery(id));

            if (result is null) return NotFound();
            return Ok(result);
        }

        // POST api/<LivrosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateLivroRequestDto requestDto)
        {
            await mediator.Send(new CreateLivroCommand(requestDto.Titulo, requestDto.Editora, requestDto.Edicao, requestDto.AnoPublicacao, requestDto.Autores, requestDto.Assuntos, requestDto.Precos));
            return Created();
        }

        // PUT api/<LivrosController>/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateLivroRequestDto requestDto)
        {
            var result = await mediator.Send(new UpdateLivroCommand(id, requestDto.Titulo, requestDto.Editora, requestDto.Edicao, requestDto.AnoPublicacao, requestDto.Autores, requestDto.Assuntos, requestDto.Precos));

            if (result.StatusCode is ErrorCode.NotFound) return NotFound();

            return NoContent();
        }

        // DELETE api/<LivrosController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteLivroCommand(id));
            return NoContent();
        }

        [HttpPatch("{id:int}/precos")]
        public async Task<IActionResult> UpdatePreco(int id, UpdateLivroPrecoRequestDto requestDto)
        {
            var result = await mediator.Send(new UpdatePrecoLivroCommand(id, requestDto.Preco, requestDto.FormaCompra));

            if (result.StatusCode is ErrorCode.NotFound) return NotFound();

            return NoContent();
        }

        // GET: api/<AutoresController>/livros
        [HttpGet("autores")]
        public async Task<IActionResult> GetByAutores()
        {
            var result = await mediator.Send(new GetLivrosGroupedByAutorReportQuery());
            return Ok(result);
        }
    }
}
