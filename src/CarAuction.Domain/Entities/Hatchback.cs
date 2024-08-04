using CarAuction.Domain.Interfaces;

namespace CarAuction.Domain.Entities;

public class Hatchback : Vehicle, ICar
{
    public int NumberOfDoors { get; set; }
}
