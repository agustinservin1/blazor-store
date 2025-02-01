using eCommerceApp.Application.Models.IdentityDto;
using eCommerceApp.Domain.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController (IAuthenticationService authenticationService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(CreateUser user)
        {
            var result = await authenticationService.CreateUser(user);
            return result.Succes ? Ok(result) : BadRequest(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser (LoginUser loginUser)
        {
            var result = await authenticationService.LoginUser(loginUser);
            return result.Succes ? Ok(result) : BadRequest(result);
        }
        [HttpGet("refreshToken/{refreshToken}")]
        public async Task<IActionResult> ReviveToken(string refreshToken)
        {
            var result= await authenticationService.ReviveToken(refreshToken);
            return result.Succes ? Ok(result) : BadRequest(result);
        }
    }
}
