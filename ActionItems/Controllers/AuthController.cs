using ActionItems.Intefaces;
using ActionItems.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ActionItems.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
         

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            string token = _authService.Login(loginRequest);
            if(token != null)
            {
                return Ok(token);
            }
            return BadRequest();
        }

        [HttpPost("AddUser")]
        public User AddUser([FromBody] User user)
        {
            var addUser = _authService.AddUser(user);
            return addUser;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddRole")]
        public Role AddRole([FromBody] Role role)
        {
            var addRole = _authService.AddRole(role);
            return addRole;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddUserRole")]
        public bool AddUserRole([FromBody] AssignRole assignRole)
        {
            return _authService.AssignRoleToUser(assignRole);
        }
    }
}
