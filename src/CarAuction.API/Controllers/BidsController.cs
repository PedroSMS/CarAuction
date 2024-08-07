using Ardalis.Result.AspNetCore;
using CarAuction.Application.Commands.CreateBid;
using CarAuction.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.API.Controllers;


[Route("api/bids")]
[ApiController]
public class BidsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Bid>> Bid([FromServices] IMediator mediator, [FromBody] CreateBidCommandRequest request)
    {
        var result = await mediator.Send(request.ToCommand());

        return this.ToActionResult(result);
    }
}
