using CarAuction.Domain.Interfaces;

namespace CarAuction.Domain.Entities;

public class Truck : Vehicle
{
    public int LoadCapacity { get; set; }
}
