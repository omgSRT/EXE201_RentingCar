using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using RentingCarRepositories.RepositoryInterface;
using RentingCarServices.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarServices.Service
{
    public class ReviewService : IReviewService
    {
        public readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public IEnumerable<ReviewImage> GetReviewImages(int page, int quantity)
        {
            if(page == 0)
            {
                page = 1;
            }
            if(quantity == 0 || quantity > int.MaxValue)
            {
                quantity = 10;
            }
            var imageList = _reviewRepository.GetReviewImages()
                .Skip((page - 1) * quantity)
                .Take(quantity);
            return imageList;
        }

        public bool AddReviewImage(IFormFile file)
        {
            ReviewImage image = null;
            return _reviewRepository.AddReviewImage(image);
        }
    }
}
