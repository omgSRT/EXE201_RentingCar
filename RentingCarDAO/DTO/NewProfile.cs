using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarDAO.DTO
{
    public class NewProfile
    {
        public string newUserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? newAddress { get; set; }
        public string? newCountry { get; set; }
        public string? newPhone { get; set; }

    }
}
