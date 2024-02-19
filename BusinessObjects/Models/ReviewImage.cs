using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessObjects.Models
{
    public partial class ReviewImage
    {
        public long ImagesId { get; set; }
        public string? ImagesLink { get; set; }
        public long? ReviewId { get; set; }

        [JsonIgnore]
        public virtual Review? Review { get; set; }
    }
}
