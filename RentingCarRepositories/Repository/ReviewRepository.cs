﻿using BusinessObjects.Models;
using RentingCarDAO;
using RentingCarRepositories.RepositoryInterface;

namespace RentingCarRepositories.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ReviewDAO _reviewDAO;
        public ReviewRepository()
        {
            _reviewDAO = new ReviewDAO();
        }

        public bool AddReview(Review review)
        {
            return _reviewDAO.AddReview(review);
        }

        public bool AddReviewImage(ReviewImage image)
        {
            return _reviewDAO.AddReviewImage(image);
        }

        public Review? GetReviewById(long id)
        {
            return _reviewDAO.GetReviewById(id);
        }

        public ReviewImage? GetReviewImageById(long id)
        {
            return _reviewDAO.GetReviewImageById(id);
        }

        public IEnumerable<ReviewImage> GetReviewImages()
        {
            return _reviewDAO.GetReviewImages();
        }

        public IEnumerable<Review> GetReviews()
        {
            return _reviewDAO.GetReviews();
        }

        public bool RemoveReview(Review review)
        {
            return _reviewDAO.RemoveReview(review);
        }

        public bool RemoveReviewImage(ReviewImage image)
        {
            return _reviewDAO.RemoveReviewImage(image);
        }

        public bool UpdateReview(Review review)
        {
            return _reviewDAO.UpdateReview(review);
        }

        public bool UpdateReviewImage(ReviewImage image)
        {
            return _reviewDAO.UpdateReviewImage(image);
        }
    }
}
