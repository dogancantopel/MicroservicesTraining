using FreeCourse.IdentityServer.Dtos;
using FreeCourse.IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FreeCourse;
using FreeCourse.Shared.Dtos;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using static IdentityServer4.IdentityServerConstants;
using System.IdentityModel.Tokens.Jwt;

namespace FreeCourse.IdentityServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
     
        [HttpPost]
        public async Task<IActionResult> Signup(SignupDto signup)
        {
            var appUser = new ApplicationUser
            {
                Email = signup.Email,
                City = signup.City,
                UserName = signup.UserName
            };
            var result = await _userManager.CreateAsync(appUser, signup.Password);
            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(m => m.Description).ToList(), 400));
            }
            return NoContent();

        }
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(m => m.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(userIdClaim.Value);

            if (user == null)
                return BadRequest();

            return Ok(new ApplicationUser
            {
                Id = user.Id,
                City = user.City,
                Email = user.Email,
                UserName = user.UserName
            });
        }
    }
}
