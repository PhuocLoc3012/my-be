using Application.Dtos.AuthDto;
using Application.IServices;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper, IJwtService jwtService)
        {
            _userManager = userManager; 
            _mapper = mapper;
            _jwtService = jwtService;
        }
        public async Task<RegistrationResponseDto> RegisterAsync(UserRegistrationDto userRegistrationDto)
        {
            
            var user = _mapper.Map<ApplicationUser>(userRegistrationDto);
            var rs = await _userManager.CreateAsync(user, userRegistrationDto.Password);
            if (!rs.Succeeded)
            {
                var errors = rs.Errors.Select(e => e.Description).ToList();
                return new RegistrationResponseDto { IsSuccessfulRegistration = false ,Errors = errors };
            }
            return new RegistrationResponseDto { IsSuccessfulRegistration = true};
        }

        public async Task<AuthReponseDto> AuthenticateAsync(UserAuthenDto userAuthenDto)
        {
            var user = await _userManager.FindByNameAsync(userAuthenDto.UserName);
            if (user is null || !await  _userManager.CheckPasswordAsync(user, userAuthenDto.Password!))
            {
                return new AuthReponseDto { IsAuthSuccessful = false, ErrorMessage = "Invalid Authentication" };
            }
            var token = _jwtService.CreateToken(user);
            return new AuthReponseDto { IsAuthSuccessful = true, Token = token };
        }
    }
}
