using System.Collections.Generic;
using System.Security.Claims;

namespace MiniProject.JWT
{
    public class UserClaimTypes
    {
        public const string EmpId = "EmpId"; //ClaimTypes.NameIdentifier;
        public const string RoleId = "RoleId";

        public static IList<Claim> GetClaims(UserDTO userDTO)
        {
            return new List<Claim>
            {
                new Claim(EmpId, userDTO.EmpId.ToString()),
                new Claim(RoleId, userDTO.RoleId.ToString())
            };
        }
    }
}
