using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarDAO
{
    public class ReviewDAO
    {
        private static ReviewDAO instance = null;
        private static exe201Context db = null;

        public ReviewDAO()
        {
            db = new exe201Context();
        }
        public static ReviewDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReviewDAO();
                }
                return instance;
            }
        }

        public IEnumerable<ReviewImage> GetReviewImages()
        {
            try
            {
                return db.Set<ReviewImage>();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public ReviewImage? GetReviewImageById(long id)
        {
            try
            {
                return db.Set<ReviewImage>().Where(x => x.ImagesId == id).FirstOrDefault();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public bool AddReviewImage(ReviewImage image)
        {
            try
            {
                if (image == null)
                {
                    return false;
                }
                db.Add(image);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public bool UpdateReviewImage(ReviewImage image)
        {
            try
            {
                if (image == null)
                {
                    return false;
                }
                if (image.ImagesLink?.Trim().Length == 0)
                {
                    return false;
                }
                var checkExist = db.Accounts.Find(image.ImagesId);
                if (checkExist != null)
                {
                    db.Entry(checkExist).State = EntityState.Detached;
                }
                db.Update(image);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public bool RemoveReviewImage(ReviewImage image)
        {
            try
            {
                if (image == null)
                {
                    return false;
                }
                var checkExist = db.Accounts.Find(image.ImagesId);
                if (checkExist != null)
                {
                    db.Entry(checkExist).State = EntityState.Detached;
                }
                db.Remove(image);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}
