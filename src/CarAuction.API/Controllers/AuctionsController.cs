using Ardalis.Result.AspNetCore;
using CarAuction.Application.Commands.CloseAuction;
using CarAuction.Application.Commands.CreateAuction;
using CarAuction.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.API.Controllers;

[Route("api/auctions")]
[ApiController]
public class AuctionsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Auction>> Create([FromServices] IMediator mediator, [FromBody] CreateAuctionCommandRequest request)
    {
        var result = await mediator.Send(request.ToCommand());

        return this.ToActionResult(result);
    }

    [HttpPut("{id}/close")]
    public async Task<ActionResult<Unit>> Close([FromServices] IMediator mediator, [FromRoute] Guid id)
    {
        var result = await mediator.Send(new CloseAuctionCommand(id));

        return this.ToActionResult(result);
    }
}
