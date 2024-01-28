using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class ImagesLicenseCard
    {
        public long ImagesId { get; set; }
        public string ImagesLink { get; set; } = null!;
        public string ImagesType { get; set; } = null!;
        public long AccountId { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
