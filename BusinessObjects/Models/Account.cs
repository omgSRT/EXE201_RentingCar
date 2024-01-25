using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class Account : IdentityUser
    {
        public Account()
        {
            Reservations = new HashSet<Reservation>();
            Reviews = new HashSet<Review>();
        }

        public long AccountId { get; set; }
        
        override
        public string UserName { get; set; } = null!;

        override
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
        public int Phone { get; set; }
        public long RoleId { get; set; }
        public long StatusId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
        public virtual AccountProfile? AccountProfile { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
