using System;
using System.Collections.Generic;
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
        public string? Description { get; set; }
        public int? Point { get; set; }
        public long VehicleId { get; set; }
        public long AccountId { get; set; }
        public long StatusId { get; set; }

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
