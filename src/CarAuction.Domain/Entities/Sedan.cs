using CarAuction.Domain.Interfaces;

namespace CarAuction.Domain.Entities;

public class Sedan : Vehicle, ICar
{
    public int NumberOfDoors { get; set; }
}
