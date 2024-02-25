using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarDAO.DTO
{
    public class VehicleDTO
    {
        public long VehicleId { get; set; }
        public string VehicleName { get; set; } = null!;
        public int Passengers { get; set; }
        public string Fueltype { get; set; } = null!;
        public double Price { get; set; }
        public string ModelType { get; set; } = null!;
        public string? TypeName { get; set; }
        public string? VehicleImage { get; set; }
    }
}
