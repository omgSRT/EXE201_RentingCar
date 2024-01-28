using System;
using System.Collections.Generic;

namespace BusinessObjects.Models
{
    public partial class ReviewImage
    {
        public long ImagesId { get; set; }
        public string? ImagesLink { get; set; }
        public long? ReviewId { get; set; }

        public virtual Review? Review { get; set; }
    }
}
