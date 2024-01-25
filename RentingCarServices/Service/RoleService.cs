using BusinessObjects.Models;
using RentingCarRepositories.RepositoryInterface;
using RentingCarServices.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentingCarServices.Service
{
    public class RoleService : IRoleService
    {
        public readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Role? GetRoleById(long id)
        {
            return _roleRepository.GetRoleById(id);
        }

        public List<Role> GetRoles()
        {
            return _roleRepository.GetRoles();
        }

        public bool Update(Role role)
        {
            return _roleRepository.Update(role);
        }
    }
}
