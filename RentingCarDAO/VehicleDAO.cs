using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;

namespace RentingCarDAO
{
    public class VehicleDAO
    {
        private static VehicleDAO instance = null;
        private static exe201Context db = null;

        public VehicleDAO()
        {
            db = new exe201Context();
        }
        public static VehicleDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VehicleDAO();
                }
                return instance;
            }
        }

        public IEnumerable<Vehicle> GetVehicles()
        {
            try
            {
                return db.Set<Vehicle>()
                    .Include(v => v.VehicleType)
                    .Include(v => v.VehicleImages)
                    .ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Vehicle? GetVehicleById(long id)
        {
            try
            {
                return db.Set<Vehicle>()
                    .Include(v => v.VehicleType)
                    .Include(v => v.VehicleImages)
                    .FirstOrDefault(x => x.VehicleId == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool AddVehicle(Vehicle vehicle)
        {
            try
            {
                if (vehicle == null)
                {
                    return false;
                }
                db.Add(vehicle);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool UpdateVehicle(Vehicle vehicle)
        {
            try
            {
                if (vehicle == null)
                {
                    return false;
                }
                var checkExist = db.ReviewImages.Find(vehicle.VehicleId);
                if (checkExist != null)
                {
                    db.Entry(checkExist).State = EntityState.Detached;
                }
                db.Update(vehicle);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public bool RemoveVehicle(Vehicle vehicle)
        {
            try
            {
                if (vehicle == null)
                {
                    return false;
                }
                var checkExist = db.ReviewImages.Find(vehicle.VehicleId);
                if (checkExist != null)
                {
                    db.Entry(checkExist).State = EntityState.Detached;
                }
                db.Remove(vehicle);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public IEnumerable<VehicleImage> GetVehicleImages()
        {
            try
            {
                return db.Set<VehicleImage>();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public VehicleImage? GetVehicleImageById(long id)
        {
            try
            {
                return db.Set<VehicleImage>().FirstOrDefault(x => x.ImagesId == id);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public bool AddVehicleImage(VehicleImage image)
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
        public bool UpdateVehicleImage(VehicleImage image)
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
                var checkExist = db.Reviews.Find(image.ImagesId);
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
        public bool RemoveVehicleImage(VehicleImage image)
        {
            try
            {
                if (image == null)
                {
                    return false;
                }
                var checkExist = db.Reviews.Find(image.ImagesId);
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
