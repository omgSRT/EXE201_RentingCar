using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarRepositories.RepositoryInterface
{
    public interface IRoleRepository
    {
        List<Role> GetRoles();
        Role? GetRoleById(long id);
        bool Update(Role role);
    }
}
