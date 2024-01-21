using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class AccountProfile
    {
        public long AccountProfileId { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public long? AccountId { get; set; }

        public virtual Account? Account { get; set; }
    }
}
