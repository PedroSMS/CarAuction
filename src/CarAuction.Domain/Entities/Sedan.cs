using CarAuction.Domain.Interfaces;

namespace CarAuction.Domain.Entities;

public class Sedan : Vehicle
{
    public int NumberOfDoors { get; set; }
}
