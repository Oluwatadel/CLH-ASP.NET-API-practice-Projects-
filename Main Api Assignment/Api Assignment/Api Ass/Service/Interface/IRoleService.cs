using Api_Ass.Conetext;
using Api_Ass.Model;

namespace Api_Ass.Service.Interface
{
    public interface IRoleService
    {
        public ICollection<Role> GetAllRoles();
        public Role GetRole(Guid id);
        public Role UpdateRole(Guid id, string RoleName);
        public void DeleteRole(Guid id);

        
    }
}
