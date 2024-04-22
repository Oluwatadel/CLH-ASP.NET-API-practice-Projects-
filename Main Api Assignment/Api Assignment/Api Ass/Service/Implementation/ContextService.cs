using Api_Ass.Conetext;
using Api_Ass.Model;
using Api_Ass.Service.Interface;

namespace Api_Ass.Service.Implementation
{
    public class ContextService : IContextService
    {
        private readonly IRoleService _roleService;

        public ContextService(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public void CreateRole()
        {
            var Admin = new Role
            {
                Name = "Admin",
                Id = Guid.Parse("b4a83ff8-f9ac-4e22-b945-f0b8845bcd46")

            };

            var Manager = new Role
            {
                Name = "Manager",
                Id = Guid.Parse("702634f3-03e1-46b6-89f7-4f233d75feea")

            };

            var Salesperson = new Role
            {
                Name = "Salesperson",
                Id = Guid.Parse("55ca7e26-bdd7-433e-b9df-0b809d42934e")

            };

            var Customer = new Role
            {
                Name = "Customer",
                Id = Guid.Parse("55ca7e16-bdd7-433e-b9df-0b809d42934e")

            };

            Context.roles.Add(Admin);
            Context.roles.Add(Manager);
            Context.roles.Add(Salesperson);
            Context.roles.Add(Customer);

            foreach (var user in Context.users)
            {
                user.Role = _roleService.GetRole(user.RoleId);
            }
        }
    }
}
