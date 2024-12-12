using ActionItems.Context;
using ActionItems.Intefaces;
using ActionItems.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ActionItems.Services
{
    public class AuthService: IAuthService
    {
        private readonly ActionContext _actionContext;
        private readonly IConfiguration _Configuration;
        public AuthService(ActionContext actionContext, IConfiguration configuration)
        {
            _actionContext = actionContext;
            _Configuration = configuration;
        }

        public Role AddRole(Role role)
        {
            var addRole = _actionContext.Roles.Add(role);
            _actionContext.SaveChanges();
            return addRole.Entity;
        }

        public User AddUser(User user)
        {
            var addUser = _actionContext.Users.Add(user);
            _actionContext.SaveChanges();
            return addUser.Entity;
        }
        public bool AssignRoleToUser(AssignRole assignRoles)
        {
            List<UserRole> addRoles = new List<UserRole>();
            var userIdExists = _actionContext.Users.SingleOrDefault(user => user.Id == assignRoles.userId);
            if (userIdExists == null)
            {
                throw new Exception("User is Not Valid");
            }
            var result = assignRoles.roleId;
            foreach (int role in assignRoles.roleId)
            {

                var userRole = new UserRole();
                userRole.roleId = role;
                userRole.userId = userIdExists.Id;
                addRoles.Add(userRole);
            }

            if (addRoles.Count > 0)
            {
                _actionContext.UserRoles.AddRange(addRoles);
                _actionContext.SaveChanges();
                return true;
            }
            return false;
        }

        public string Login(LoginRequest loginRequest)
        {
            if (loginRequest != null && loginRequest.Password != null)
            {
                var userExit = _actionContext.Users.SingleOrDefault(s => s.Email == loginRequest.Email && s.password == loginRequest.Password);
                if (userExit != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _Configuration["Jwt:Subject"]),
                        new Claim("Id",userExit.Id.ToString()),
                        new Claim("Email",userExit.Email)

                    };
                    var userRoles = _actionContext.UserRoles.Where(u => u.userId == userExit.Id).ToList();
                    var roleIds = userRoles.Select(userRole => userRole.roleId).ToList();
                    var roles = _actionContext.Roles.Where(r => roleIds.Contains(r.Id)).ToList();
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _Configuration["Jwt:Issuer"],
                        _Configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;
                }

            }
            return null;
        }
    }
}
