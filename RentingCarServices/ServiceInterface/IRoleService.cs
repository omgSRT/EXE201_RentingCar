using BusinessObjects.Models;

namespace RentingCarServices.ServiceInterface
{
    public interface IRoleService
    {
        List<Role> GetRoles();
        Role? GetRoleById(long id);
        bool Add(Role role);
        bool Update(Role role);
    }
}
