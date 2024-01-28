using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Key]
        public long AccountId { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string Phone { get; set; } = null!;
        public long RoleId { get; set; }
        public long StatusId { get; set; }

        public virtual Role? Role { get; set; } = null!;
        public virtual Status? Status { get; set; } = null!;
        public virtual ICollection<ImagesLicenseCard> ImagesLicenseCards { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
