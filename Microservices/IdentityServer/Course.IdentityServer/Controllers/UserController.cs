using Course.Common.Dtos;
using Course.IdentityServer.Dtos;
using Course.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Course.IdentityServer.Controllers {
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }
        /// <summary>
        /// Sign up 
        /// </summary>
        /// <param name="userRegister"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegister) {
            var applicationUser = new ApplicationUser {
                UserName = userRegister.UserName,
                Email = userRegister.Email,
                City = userRegister.City
            };
            var result = await _userManager.CreateAsync(applicationUser, userRegister.Password);
            if (!result.Succeeded) {
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(i => i.Description).ToList(), 400));
            }
            return NoContent();
        }
        /// <summary>
        /// Retuns User Informations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUser() {
            var userClaims = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (userClaims == null) {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(userClaims.Value);

            if (user == null) {
                return BadRequest();
            }
            var applicationUser = new ApplicationUser {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                City = user.City,
                PhoneNumber = user.PhoneNumber,
                NormalizedEmail = user.NormalizedEmail,
                NormalizedUserName = user.NormalizedUserName,
            };
            return Ok(applicationUser);
        }
    }
}
