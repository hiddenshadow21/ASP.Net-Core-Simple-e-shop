using AspNetCore.Identity.MongoDbCore.Models;

namespace MVCApp.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : MongoIdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public override string UserName { get => base.UserName; set => base.UserName = value; }
        public override string Email { get => base.Email; set => base.Email = value; }
    }
}
