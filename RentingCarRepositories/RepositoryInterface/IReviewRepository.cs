using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarRepositories.RepositoryInterface
{
    public interface IReviewRepository
    {
        IEnumerable<ReviewImage> GetReviewImages();
        ReviewImage? GetReviewImageById(long id);
        bool AddReviewImage(ReviewImage image);
        bool UpdateReviewImage(ReviewImage image);
        bool RemoveReviewImage(ReviewImage image);
    }
}
