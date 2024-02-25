using System.ComponentModel.DataAnnotations;

namespace RentingCarAPI.ViewModel
{
    public class ReviewRequestVM
    {
        [MaxLength(int.MaxValue)]
        public string? Description { get; set; }
        [Required]
        [Range(0, 5)]
        public int? Point { get; set; }
        [Required]
        public long VehicleId { get; set; }
        [Required]
        public long AccountId { get; set; }
        [Required]
        [Range(1, 2)]
        public long StatusId { get; set; }
        public List<IFormFile>? Files { get; set; }
    }
    public class AccountRequestVM
    {
        public string UserName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public IFormFile? LicenseImage { get; set; }
        public IFormFile? IdentityImage { get; set; }
    }

    public class VehicleDTO
    {
        [Required]
        public string VehicleName { get; set; } = null!;
        [Required]
        [Range(2, 60)]
        public int Passengers { get; set; }
        public string Suitcase { get; set; } = null!;
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
        [Range(0, double.MaxValue)]
        public double Deposit { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [Required]
        [MaxLength(50)]
        public string LicensePlate { get; set; } = null!;
        [Required]
        public string ModelType { get; set; } = null!;
        [Required]
        public string Location { get; set; } = null!;
        [Required]
        public long VehicleTypeId { get; set; }
        [Required]
        public List<IFormFile> VehicleImages { get; set; }
    }
}
