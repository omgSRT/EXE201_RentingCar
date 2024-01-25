using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarServices.ServiceInterface
{
    public interface IVehicleTypeService
    {
        List<VehicleType> GetVehicleTypes();
        VehicleType GetVehicleTypeByName(string searchType);
        VehicleType GetVehicleTypeById(long id);
        bool Add(VehicleType vehicleType);
        bool Update(VehicleType vehicleType);
        bool Delete(VehicleType vehicleType);
    }
}
