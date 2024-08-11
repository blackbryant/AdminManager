using AdminManager.Utility;
using AdminManager.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace AdminManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {

        private JwtHelpers jwt;

        public LoginController(JwtHelpers jwt) 
        {
            this.jwt = jwt;
        }

       
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            
            return Ok("成功");

        }

        [AllowAnonymous]
        [HttpPost("checkLogin")]
        
        public async Task<IActionResult> LoginCheck( LoginViewModel loginViewModel) 
        {
            if (loginViewModel == null) {
                return BadRequest("找不到使用者");
            }

            if (string.IsNullOrEmpty(loginViewModel.Username) || string.IsNullOrEmpty(loginViewModel.Password))
            {
                return  BadRequest("找不到使用者");
            }


            if (ValidateUser(loginViewModel)) {

                var token = jwt.GenerateToken(loginViewModel.Username);
                return Ok(new { token });
            }

            return BadRequest("驗證失敗");

        
        }


        [HttpPost("logout")]

        public async Task<IActionResult> Logout(ClaimsPrincipal user) {

            var jwt_id = user.Claims.FirstOrDefault(p => p.Type == "jti")?.Value;
            return Ok("logout");
        }




        private bool ValidateUser(LoginViewModel login)
        {
            if (login.Username == "jack" && login.Password=="123456")
            {
                return true;
            }

            return false;
        }

    }
}
