using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MiniProject.DTOs;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MiniProject.JWT
{
    public class JWTService
    {
        public IConfiguration configuration;
        public JWTService(IConfiguration config)
        {
            configuration = config;
        }
        public string GenerateJSONWebToken(UserDTO userDTO)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(UserClaimTypes.EmpId, userDTO.EmpId.ToString()),
                new Claim(UserClaimTypes.RoleId, userDTO.RoleId.ToString()),
            };

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
              configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);
            var tokenGen = new JwtSecurityTokenHandler().WriteToken(token);
            //var res = new AuthResp { Token = tokenGen };
            return tokenGen;
        }

        public UserDTO DecodeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenVal = handler.ReadToken(token) as JwtSecurityToken;
            UserDTO userDTO = new UserDTO();
            userDTO.EmpId = Convert.ToInt32(tokenVal.Claims.First(c => c.Type == UserClaimTypes.EmpId).Value);
            userDTO.RoleId = Convert.ToInt32( tokenVal.Claims.First(c => c.Type == UserClaimTypes.RoleId).Value);
            return userDTO;
        }

    }
}
