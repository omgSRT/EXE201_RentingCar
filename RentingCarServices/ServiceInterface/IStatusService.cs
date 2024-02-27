using BusinessObjects.Models;

namespace RentingCarServices.ServiceInterface
{
    public interface IStatusService
    {
        List<Status> GetStatuses();
        Status? GetStatusById(long id);
        bool Update(Status status);
    }
}
