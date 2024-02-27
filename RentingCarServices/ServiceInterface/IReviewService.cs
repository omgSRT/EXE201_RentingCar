﻿using BusinessObjects.Models;

namespace RentingCarServices.ServiceInterface
{
    public interface IReviewService
    {
        bool AddReviewImage(ReviewImage image);
        IEnumerable<ReviewImage> GetReviewImages(int page, int quantity);
        IEnumerable<Review> GetReviews(int page, int quantity, int? filterPoint);
        Review? GetReviewById(long id);
        bool AddReview(Review review);
        bool UpdateReview(Review review);
        bool RemoveReview(Review review);
    }
}
