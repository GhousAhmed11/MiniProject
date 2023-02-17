using Microsoft.AspNetCore.Mvc.Filters;
using MiniProject.JWT;
using MiniProject.Models;
using MiniProject.Repository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MiniProject.ApiAuthorize
{
    public class ApiAuthorize : Attribute, IAsyncActionFilter
    {
        private static readonly string ApiKeyHeaderName = "Authorization";
        private AppUserRepo  appUserRepo { get; set; }
        public string[] Roles { get; }
        public ApiAuthorize(params string[] roles)
        {
            Roles = roles;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var jwtTokenService = context.HttpContext.RequestServices.GetService(typeof(JWTService)) as JWTService;
            var sessionHelper = context.HttpContext.RequestServices.GetService(typeof(SessionHelper)) as SessionHelper;
            appUserRepo = context.HttpContext.RequestServices.GetService(typeof(AppUserRepo)) as AppUserRepo;

            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var requestHeaderApiValue))
            {
                await UnauthorizedRsp();
            }
            string jwtToken = requestHeaderApiValue.ToString().Split(' ')[1];
            var handler = new JwtSecurityTokenHandler();
            var tokenObj = handler.ReadToken(jwtToken) as JwtSecurityToken;
            var EmpId  = tokenObj.Claims.First(o => o.Type == UserClaimTypes.EmpId).Value;
            _ = EmpId == "" ? EmpId = "0" : EmpId;
            //if (EmpId == "")
            //{
            //    EmpId = "0";
            //}
            sessionHelper.EmpId = Convert.ToInt32(EmpId) ;
            sessionHelper.RoleId = Convert.ToInt32(tokenObj.Claims.First(o => o.Type == UserClaimTypes.RoleId).Value);
            sessionHelper.Token = jwtToken;

            if (Roles.Count() != 0)
            {
                await OnAuthorization(Roles, context);
            }
            await next();
        }
        public async Task OnAuthorization(string[] Roles, ActionExecutingContext context)
        {
            var sessionHelper = context.HttpContext.RequestServices.GetService(typeof(SessionHelper)) as SessionHelper;

            foreach (var role in Roles)
            {
                if (role == ((enRole)sessionHelper.RoleId).ToString())
                {
                    return;
                }
            }
            await UnauthorizedRsp();
        }

        public Task UnauthorizedRsp(bool isTokenExpired = false)
        {
            if (isTokenExpired)
                throw new ServiceException("Token Expired");
            else
                throw new ServiceException("UnAuthorize");
        }
    }
}
