using BusinessObjects.Models;
using RentingCarDAO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarServices.ServiceInterface
{
    public interface IVehicleService
    {
        public List<VehicleDTO> GetAllVehicles();
        public Vehicle? GetVehicleById(long id);
        public bool AddVehicle(Vehicle vehicle);
        bool AddVehicleImage(VehicleImage image);
    }
}
