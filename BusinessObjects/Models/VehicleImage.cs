using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models
{
    public partial class VehicleImage
    {
        public long ImagesId { get; set; }
        [Required]
        public string? ImagesLink { get; set; }
        public long? VehicleId { get; set; }

        [JsonIgnore]
        public virtual Vehicle? Vehicle { get; set; }
    }
}
