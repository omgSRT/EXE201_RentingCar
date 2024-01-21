using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Image
    {
        public long ImagesId { get; set; }
        public string? ImagesLink { get; set; }
        public long? VehicleId { get; set; }
        public long? ReviewId { get; set; }

        public virtual Review? Review { get; set; }
        public virtual Vehicle? Vehicle { get; set; }
    }
}
