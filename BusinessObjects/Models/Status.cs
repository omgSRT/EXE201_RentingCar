using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models
{
    public partial class Status
    {
        public Status()
        {
            Accounts = new HashSet<Account>();
            Reviews = new HashSet<Review>();
            Vehicles = new HashSet<Vehicle>();
        }

        public long StatusId { get; set; }
        public string? StatusName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
