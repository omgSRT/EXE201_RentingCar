using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models
{
    public partial class Review
    {
        public Review()
        {
            ReviewImages = new HashSet<ReviewImage>();
        }

        public long ReviewId { get; set; }
        [Range(1, int.MaxValue)]
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

        [JsonIgnore]
        public virtual Account? Account { get; set; } = null!;
        [JsonIgnore]
        public virtual Status? Status { get; set; } = null!;
        [JsonIgnore]
        public virtual Vehicle? Vehicle { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<ReviewImage>? ReviewImages { get; set; }
    }
}
