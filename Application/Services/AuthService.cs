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
        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager; 
            _mapper = mapper;
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
    }
}
