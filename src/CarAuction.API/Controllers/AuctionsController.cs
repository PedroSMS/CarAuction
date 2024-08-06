﻿using CarAuction.Application.Commands.CloseAuction;
using CarAuction.Application.Commands.CreateAuction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.API.Controllers;

[Route("api/auctions")]
[ApiController]
public class AuctionsController : ControllerBase
{
    // Just a placeholder for the method 'CreatedAtAction'
    [HttpGet("{id}")]
    public IActionResult GetById()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromServices] IMediator mediator, [FromBody] CreateAuctionCommandRequest request)
    {
        var auction = await mediator.Send(request.ToCommand());

        return CreatedAtAction(nameof(GetById), new { id = auction.Id }, auction);
    }

    [HttpPut("{id}/close")]
    public async Task<IActionResult> Close([FromServices] IMediator mediator, [FromRoute] Guid id)
    {
        await mediator.Send(new CloseAuctionCommand(id));

        return NoContent();
    }
}
