using CarAuction.Application.Commands.CreateBid;
using CarAuction.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.API.Controllers;


[Route("api/bids")]
[ApiController]
public class BidsController : ControllerBase
{
    // Just a placeholder for the method 'CreatedAtAction'
    [HttpGet("{id}")]
    public IActionResult GetById()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Bid([FromServices] IMediator mediator, [FromBody] CreateBidCommandRequest request)
    {
        var bid = await mediator.Send(request.ToCommand());

        return CreatedAtAction(nameof(GetById), new { id = bid.Id }, bid);
    }
}
