using Application.Dtos.AuthDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IAuthService
    {
        Task<RegistrationResponseDto> RegisterAsync(UserRegistrationDto userRegistrationDto);
        Task<AuthReponseDto> AuthenticateAsync(UserAuthenDto userAuthenDto);
    }
}
