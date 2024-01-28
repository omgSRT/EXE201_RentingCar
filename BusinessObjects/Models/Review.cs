using System;
using System.Collections.Generic;

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

        public virtual Account Account { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
        public virtual Vehicle Vehicle { get; set; } = null!;
        public virtual ICollection<ReviewImage> ReviewImages { get; set; }
    }
}
