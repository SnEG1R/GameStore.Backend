﻿using AutoMapper;
using GameStore.Application.CQs.Genre.Commands.Create;
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

    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] CreateGenreDto genre)
    {
        var command = _mapper.Map<CreateGenreCommand>(genre);
        var genreId = await Mediator.Send(command);
        
        return Created("api/genres", genreId);
    }
}