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
        public long? StatusId { get; set; }
        public List<IFormFile>? Files { get; set; }
    }
}
