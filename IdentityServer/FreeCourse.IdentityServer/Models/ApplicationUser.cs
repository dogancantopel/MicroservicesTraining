using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FreeCourse.IdentityServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [StringLength(400)]
        public string City { get; set; }
    }
}
