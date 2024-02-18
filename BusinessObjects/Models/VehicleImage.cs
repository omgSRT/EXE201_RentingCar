using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class VehicleImage
    {
        public long ImagesId { get; set; }
        public string? ImagesLink { get; set; }
        public long? VehicleId { get; set; }

        public virtual Vehicle? Vehicle { get; set; }
    }
}
