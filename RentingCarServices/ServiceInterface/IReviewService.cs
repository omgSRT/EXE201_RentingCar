using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarServices.ServiceInterface
{
    public interface IReviewService
    {
        bool AddReviewImage(IFormFile file);
        IEnumerable<ReviewImage> GetReviewImages(int page, int quantity);
    }
}
