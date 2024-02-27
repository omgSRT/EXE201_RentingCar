﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        public long RoleId { get; set; }
        [Required]
        [MinLength(1), MaxLength(50)]
        public string? RoleName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Account>? Accounts { get; set; }
    }
}
