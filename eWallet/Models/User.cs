using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eWallet.Models {
    public class User : IdentityUser
    {
        public UserType UserRole { get; set; }

        public MembershipStatus Status { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public enum MembershipStatus
    {
        Pending, Approved, Suspended
    }

    public enum UserType
    {
        Admin, Agent, Farmer
    }
}