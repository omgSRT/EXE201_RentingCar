using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models
{
    public partial class VehicleType
    {
        public VehicleType()
        {
            Vehicles = new HashSet<Vehicle>();
        }

        public long VehicleTypeId { get; set; }
        [Required]
        [MinLength(1), MaxLength(50)]
        public string? TypeName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Vehicle>? Vehicles { get; set; }
    }
}
