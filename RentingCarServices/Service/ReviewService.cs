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
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public IEnumerable<ReviewImage> GetReviewImages(int page, int quantity)
        {
            if(page <= 0)
            {
                page = 1;
            }
            if(quantity <= 0 || quantity > int.MaxValue)
            {
                quantity = 10;
            }
            var imageList = _reviewRepository.GetReviewImages()
                .Skip((page - 1) * quantity)
                .Take(quantity);
            return imageList;
        }

        public bool AddReviewImage(ReviewImage image)
        {
            return _reviewRepository.AddReviewImage(image);
        }

        public IEnumerable<Review> GetReviews(int page, int quantity, int? filterPoint)
        {
            IEnumerable<Review> reviewList = new List<Review>();
            if (page <= 0)
            {
                page = 1;
            }
            if (quantity <= 0 || quantity > int.MaxValue)
            {
                quantity = 10;
            }
            if(filterPoint != null)
            {
                reviewList = _reviewRepository.GetReviews()
                    .Where(review => review.StatusId == 1)
                    .Where(review => review.Point == filterPoint)
                    .Skip((page - 1) * quantity)
                    .Take(quantity)
                    .ToList();
            }
            else
            {
                reviewList = _reviewRepository.GetReviews()
                    .Where(review => review.StatusId == 1)
                    .Skip((page - 1) * quantity)
                    .Take(quantity)
                    .ToList();
            }
            return reviewList;
        }

        public Review? GetReviewById(long id)
        {
            return _reviewRepository.GetReviewById(id);
        }

        public bool AddReview(Review review)
        {
            return _reviewRepository.AddReview(review);
        }

        public bool UpdateReview(Review review)
        {
            return _reviewRepository.UpdateReview(review);
        }

        public bool RemoveReview(Review review)
        {
            return _reviewRepository.RemoveReview(review);
        }
    }
}
