using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarRepositories.RepositoryInterface
{
    public interface IVehicleTypeRepository
    {
        List<VehicleType> GetVehicleTypes();
        VehicleType GetVehicleTypeByName(string searchType);
        VehicleType GetVehicleTypeById(long id);
        bool Add(VehicleType vehicleType);
        bool Update(VehicleType vehicleType);
        bool Delete(VehicleType vehicleType);
    }
}
