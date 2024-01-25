using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarServices.ServiceInterface
{
    public interface IStatusService
    {
        List<Status> GetStatuses();
        Status? GetStatusById(long id);
        bool Update(Status status);
    }
}
