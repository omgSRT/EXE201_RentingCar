using BusinessObjects.Models;
using RentingCarDAO;
using RentingCarRepositories.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarRepositories.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ReviewDAO _reviewDAO;
        public ReviewRepository()
        {
            _reviewDAO = new ReviewDAO();
        }
        public bool AddReviewImage(ReviewImage image)
        {
            return _reviewDAO.AddReviewImage(image);
        }

        public ReviewImage? GetReviewImageById(long id)
        {
            return _reviewDAO.GetReviewImageById(id);
        }

        public IEnumerable<ReviewImage> GetReviewImages()
        {
            return _reviewDAO.GetReviewImages();
        }

        public bool RemoveReviewImage(ReviewImage image)
        {
            return _reviewDAO.RemoveReviewImage(image);
        }

        public bool UpdateReviewImage(ReviewImage image)
        {
            return _reviewDAO.UpdateReviewImage(image);
        }
    }
}
