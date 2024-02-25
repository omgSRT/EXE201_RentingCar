using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Models
{
    public partial class ImagesLicenseCard
    {
        public long ImagesId { get; set; }
        [Required]
        public string ImagesLink { get; set; } = null!;
        [Required]
        public string ImagesType { get; set; } = null!;
        public long AccountId { get; set; }

        public virtual Account? Account { get; set; } = null!;
    }
}
