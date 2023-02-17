using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject.DTOs;
using MiniProject.JWT;
using MiniProject.Repository.Interfaces;
using System.Threading.Tasks;
using Syncfusion.XlsIO;
using System.IO;
using Syncfusion.Drawing;

namespace MiniProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserRepo _appUserRepo;

        
        public AppUserController(IAppUserRepo appUserRepo)
        {
            _appUserRepo = appUserRepo;
        }
        [HttpPost("register")]
        public async Task<AppUserDTO> Register(RegistrationDTO registrationDTO)
        {
            var res = await _appUserRepo.Register(registrationDTO);
            return res;
        }
        [HttpPost("login")]
        public async Task<string> Login(LoginDTO loginDTO)
        {
            var res = await _appUserRepo.Login(loginDTO);
            return res;
        }

        [HttpPost("decode")]
        public UserDTO DecodeToken(string token)
        {
            var res = _appUserRepo.Decode(token);
            return res;
        }

        [HttpGet("excel")]
        public FileStreamResult excel()
        {
            var res = _appUserRepo.export();
            return res;
        }


    }
}
