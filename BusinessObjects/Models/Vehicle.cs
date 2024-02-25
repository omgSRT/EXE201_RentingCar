using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [Required]
        public string VehicleName { get; set; } = null!;
        [Required]
        [Range(2, 60)]
        public int Passengers { get; set; }
        [Required]
        public string Suitcase { get; set; } = null!;
        [Required]
        [Range(0, 10)]
        public int Doors { get; set; }
        [Required]
        public string Engine { get; set; } = null!;
        [Required]
        public string Fueltype { get; set; } = null!;
        [Required]
        public string Options { get; set; } = null!;
        [Required]
        [Range(1, int.MaxValue)]
        public int Amount { get; set; }
        [Required]
        [Range(10, double.MaxValue)]
        public double Deposit { get; set; }
        [Required]
        [Range(10, double.MaxValue)]
        public double Price { get; set; }
        [Required]
        [MaxLength(50)]
        public string LicensePlate { get; set; } = null!;
        [Required]
        public string ModelType { get; set; } = null!;
        [Required]
        public string Location { get; set; } = null!;
        public long StatusId { get; set; }
        [JsonIgnore]
        public long? ReservationId { get; set; }
        [Required]
        [JsonIgnore]
        public long VehicleTypeId { get; set; }

        [JsonIgnore]
        public virtual Reservation? Reservation { get; set; }
        [JsonIgnore]
        public virtual Status? Status { get; set; } = null!;
        
        public virtual VehicleType? VehicleType { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Review>? Reviews { get; set; }
        
        public virtual ICollection<VehicleImage>? VehicleImages { get; set; }
    }
}
