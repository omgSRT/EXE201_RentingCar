using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarDAO
{
    public class VehicleTypeDAO
    {
        private static VehicleTypeDAO instance = null;
        private static exe201Context db = null;

        public VehicleTypeDAO()
        {
            db = new exe201Context();
        }
        public static VehicleTypeDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VehicleTypeDAO();
                }
                return instance;
            }
        }

        public List<VehicleType> GetVehicleTypes()
        {
            try
            {
                return db.Set<VehicleType>().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public VehicleType GetVehicleTypeByName(string searchType)
        {
            try
            {
                if(searchType == null)
                {
                    return new VehicleType();
                }
                return db.Set<VehicleType>().Where(x => x.TypeName.Contains(searchType)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public VehicleType GetVehicleTypeById(long id)
        {
            try
            {
                if (id == null)
                {
                    return new VehicleType();
                }
                return db.Set<VehicleType>().Where(x => x.VehicleTypeId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public bool Add(VehicleType vehicleType)
        {
            try
            {
                if (vehicleType == null || vehicleType.TypeName.Trim().Equals(string.Empty))
                {
                    return false;
                }
                if(vehicleType.TypeName.Trim().Length > 50) {
                    return false;
                }
                VehicleType existVehicleType = db.Set<VehicleType>()
                    .FirstOrDefault(x => x.TypeName.Equals(vehicleType.TypeName));
                if (existVehicleType != null)
                {
                    return false;
                }
                db.Add(vehicleType);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }
        public bool Update(VehicleType vehicleType)
        {
            try
            {
                if (vehicleType == null || vehicleType.TypeName.Trim().Equals(string.Empty))
                {
                    return false;
                }
                if (vehicleType.TypeName.Trim().Length > 50)
                {
                    return false;
                }
                VehicleType existVehicleType = db.Set<VehicleType>()
                    .FirstOrDefault(x => x.TypeName.Equals(vehicleType.TypeName));
                if (existVehicleType != null)
                {
                    return false;
                }
                var checkExist = db.Accounts.Find(vehicleType.VehicleTypeId);
                if (checkExist != null)
                {
                    db.Entry(checkExist).State = EntityState.Detached;
                }
                db.Update(vehicleType);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public bool Remove(VehicleType vehicleType)
        {
            try
            {
                if(vehicleType == null)
                {
                    return false;
                }
                var checkExist = db.Accounts.Find(vehicleType.VehicleTypeId);
                if (checkExist != null)
                {
                    db.Entry(checkExist).State = EntityState.Detached;
                }
                db.Remove(vehicleType);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
