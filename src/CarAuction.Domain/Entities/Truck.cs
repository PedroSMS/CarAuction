using CarAuction.Domain.Interfaces;

namespace CarAuction.Domain.Entities;

public class Truck : Vehicle, ITruck
{
    public decimal LoadCapacity { get; set; }
}
