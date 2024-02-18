using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Reviews = new HashSet<Review>();
            VehicleImages = new HashSet<VehicleImage>();
        }

        public long VehicleId { get; set; }
        public string VehicleName { get; set; } = null!;
        public int Passengers { get; set; }
        public string Suitcase { get; set; } = null!;
        public int Doors { get; set; }
        public string Engine { get; set; } = null!;
        public string Fueltype { get; set; } = null!;
        public string Options { get; set; } = null!;
        public int Amount { get; set; }
        public double Deposit { get; set; }
        public double Price { get; set; }
        public string LicensePlate { get; set; } = null!;
        public long StatusId { get; set; }
        public long? ReservationId { get; set; }
        public long VehicleTypeId { get; set; }

        public virtual Reservation? Reservation { get; set; }
        public virtual Status Status { get; set; } = null!;
        public virtual VehicleType VehicleType { get; set; } = null!;
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<VehicleImage> VehicleImages { get; set; }
    }
}
