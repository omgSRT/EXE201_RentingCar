﻿using BusinessObjects.Models;

namespace RentingCarRepositories.RepositoryInterface
{
    public interface IReviewRepository
    {
        IEnumerable<ReviewImage> GetReviewImages();
        ReviewImage? GetReviewImageById(long id);
        bool AddReviewImage(ReviewImage image);
        bool UpdateReviewImage(ReviewImage image);
        bool RemoveReviewImage(ReviewImage image);
        IEnumerable<Review> GetReviews();
        Review? GetReviewById(long id);
        bool AddReview(Review review);
        bool UpdateReview(Review review);
        bool RemoveReview(Review review);
    }
}
