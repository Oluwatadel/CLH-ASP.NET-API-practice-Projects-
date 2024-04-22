using Api_Ass.Conetext;
using Api_Ass.Model;
using Api_Ass.Service.Interface;

namespace Api_Ass.Service.Implementation
{
    public class RoleService : IRoleService
    {
        public void DeleteRole(Guid id)
        {
            var role = Context.roles.FirstOrDefault(x => x.Id == id);
            Context.roles.Remove(role!);
        }

        public ICollection<Role> GetAllRoles()
        {
            return Context.roles;

        }

        public Role GetRole(Guid id)
        {
            var role = Context.roles.FirstOrDefault(x => x.Id == id);
            if(role == null)
            {
                return null;
            }
            return role!;
        }

        public Role UpdateRole(Guid id, string newRoleName)
        {
            var role = GetRole(id);
            role.Name = newRoleName;
            return role;
        }
    }
}
