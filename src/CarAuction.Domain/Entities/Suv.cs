using CarAuction.Domain.Interfaces;

namespace CarAuction.Domain.Entities;

public class Suv: Vehicle, ISuv
{
    public int NumberOfSeats { get; set; }
}
