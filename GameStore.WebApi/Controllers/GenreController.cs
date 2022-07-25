﻿using AutoMapper;
using GameStore.Application.CQs.Genre.Commands.Create;
using GameStore.Application.CQs.Genre.Commands.Delete;
using GameStore.Application.CQs.Genre.Commands.Update;
using GameStore.Application.CQs.Genre.Queries.GetGenre;
using GameStore.WebApi.Models.Genre;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers;

[Route("api/genres")]
public class GenreController : BaseController
{
    private readonly IMapper _mapper;

    public GenreController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult> Get(long id)
    {
        var query = new GetGenreQuery()
        {
            Id = id
        };
        var genre = await Mediator.Send(query);
        
        return Ok(genre);
    }

    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] CreateGenreDto genre)
    {
        var command = _mapper.Map<CreateGenreCommand>(genre);
        var genreId = await Mediator.Send(command);
        
        return Created("api/genres", genreId);
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult> Update(long id, [FromBody] UpdateGenreDto genre)
    {
        var command = _mapper.Map<UpdateGenreCommand>(genre);
        command.Id = id;
        await Mediator.Send(command);
        
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var command = new DeleteGenreCommand()
        {
            Id = id
        };
        await Mediator.Send(command);
        
        return NoContent();
    }
}