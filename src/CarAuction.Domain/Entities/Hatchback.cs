using CarAuction.Domain.Interfaces;

namespace CarAuction.Domain.Entities;

public class HatchBack : Vehicle, ICar
{
    public int NumberOfDoors { get; set; }
}
