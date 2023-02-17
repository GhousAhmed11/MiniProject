using Microsoft.AspNetCore.Mvc;
using MiniProject.DTOs;
using MiniProject.JWT;
using System.Threading.Tasks;

namespace MiniProject.Repository.Interfaces
{
    public interface IAppUserRepo
    {
        Task<AppUserDTO> Register(RegistrationDTO registrationDTO);

        Task<string> Login(LoginDTO loginDTO);

        UserDTO Decode(string token);
        FileStreamResult export();
        void UpdateExcelSheetData();
    }
}
