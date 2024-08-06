﻿using CarAuction.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Application.Commands.CreateAuction;

public class CreateAuctionCommandValidator : AbstractValidator<CreateAuctionCommand>
{
    public CreateAuctionCommandValidator(ICarAuctionContext db)
    {
        RuleFor(e => e.CarId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await ExistsInDatabaseAsync(db, id, ct))
            .WithMessage("Car does not exists in database.")
            .MustAsync(async (id, ct) => await DoNotExistInActiveAuctionAsync(db, id, ct))
            .WithMessage("Car is already in an active auction.");
    }

    #region private
    private static async Task<bool> ExistsInDatabaseAsync(
        ICarAuctionContext db, Guid id, CancellationToken cancellationToken)
    {
        return await db.Vehicle
            .AnyAsync(e => e.Id == id, cancellationToken);
    }

    private static async Task<bool> DoNotExistInActiveAuctionAsync(
        ICarAuctionContext db, Guid id, CancellationToken cancellationToken)
    {
        return await db.Auction
            .AnyAsync(e => e.CarId == id && !e.FinishedAtUtc.HasValue, 
                cancellationToken) is false;
    }
    #endregion
}