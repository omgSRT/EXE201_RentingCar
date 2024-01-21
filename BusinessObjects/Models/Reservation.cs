using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Reservation
    {
        public Reservation()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public long ReservationId { get; set; }
        public string PickupLocation { get; set; } = null!;
        public string ReturnLocation { get; set; } = null!;
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool RoadTax { get; set; }
        public bool ComprehensiveInsurance { get; set; }
        public bool UnlimitedMileage { get; set; }
        public bool SecurityDeposit { get; set; }
        public bool BabySeat { get; set; }
        public bool BreakdownAssistance { get; set; }
        public int Amount { get; set; }
        public int Status { get; set; }
        public double TotalPrice { get; set; }
        public long AccountId { get; set; }

        public virtual Account Account { get; set; } = null!;
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
