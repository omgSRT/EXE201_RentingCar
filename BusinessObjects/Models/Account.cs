using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models
{
    public partial class Account
    {
        public Account()
        {
            ImagesLicenseCards = new HashSet<ImagesLicenseCard>();
            Reservations = new HashSet<Reservation>();
            Reviews = new HashSet<Review>();
        }

        public long AccountId { get; set; }
        public string UserName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string? Password { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public long RoleId { get; set; }
        public long StatusId { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; } = null!;
        [JsonIgnore]
        public virtual Status Status { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<ImagesLicenseCard> ImagesLicenseCards { get; set; }
        [JsonIgnore]
        public virtual ICollection<Reservation> Reservations { get; set; }
        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
