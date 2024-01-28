using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class VehicleType
    {
        public VehicleType()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public long VehicleTypeId { get; set; }
        public string? TypeName { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
