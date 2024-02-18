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

        public bool AddReviewImage(ReviewImage image)
        {
            return _reviewRepository.AddReviewImage(image);
        }

        public IEnumerable<Review> GetReviews(int page, int quantity)
        {
            if (page == 0)
            {
                page = 1;
            }
            if (quantity == 0 || quantity > int.MaxValue)
            {
                quantity = 10;
            }
            var reviewList = _reviewRepository.GetReviews()
                .Skip((page - 1) * quantity)
                .Take(quantity);
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

        public int GetLastInsertedReviewId()
        {
            return _reviewRepository.GetLastInsertedReviewId();
        }
    }
}
