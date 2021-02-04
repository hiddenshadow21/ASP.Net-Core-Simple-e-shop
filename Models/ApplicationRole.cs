using AspNetCore.Identity.MongoDbCore.Models;

namespace MVCApp.Models
{
    public class ApplicationRole : MongoIdentityRole
    {
        public ApplicationRole() : base()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
