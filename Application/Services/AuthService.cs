using Application.Common.Exceptions;
using Application.Dtos.AuthDto;
using Application.IServices;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
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
        private readonly IEmailSender _emailSender; 
        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper, IJwtService jwtService, IEmailSender emailSender)
        {
            _userManager = userManager; 
            _mapper = mapper;
            _jwtService = jwtService;
            _emailSender = emailSender;
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

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var param = new Dictionary<string, string?>
            {
                { "token", token},
                { "email", user.Email }
            };
            var callBack = QueryHelpers.AddQueryString(userRegistrationDto.ClientUri!, param);
 
            await _emailSender.SendEmailAsync(user.Email, "Email Confirmation", callBack);
            
            await _userManager.AddToRoleAsync(user, "Customer");
            return new RegistrationResponseDto { IsSuccessfulRegistration = true};
        }

        public async Task<AuthReponseDto> AuthenticateAsync(UserAuthenDto userAuthenDto)
        {
           
            var user = await _userManager.FindByNameAsync(userAuthenDto.UserName);
            if (user is null || !await  _userManager.CheckPasswordAsync(user, userAuthenDto.Password!))
            {
                throw new BadRequestException("Invalid authentication");
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new BadRequestException("Email is not confirmed");
            }
            var roles = await _userManager.GetRolesAsync(user);

            var token =  _jwtService.CreateToken(user, roles, true);
            await _userManager.UpdateAsync(user);
            return new AuthReponseDto { IsAuthSuccessful = true, Token = token };
        }

        public async Task EmailConfirmationAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                throw new NotFoundException("User not found - Invalid email confirmation request");
            }
            var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
            if (!confirmResult.Succeeded)
            {
                // Check for specific errors related to token validation
                foreach (var error in confirmResult.Errors)
                {
                    if (error.Code == "InvalidToken")
                    {
                        throw new InvalidOperationException("The token is invalid.");
                    }
                    if (error.Code == "TokenExpired")
                    {
                        throw new InvalidOperationException("The token has expired.");
                    }
                }

                // If other errors, throw a general exception
                throw new BadRequestException("Invalid email confirmation request");
            }
        }

        public async Task<TokenDto> RefreshTokenAsync(TokenDto tokenDto)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
            var roles = await _userManager.GetRolesAsync(user);
            if (user is null || user.RefreshToken != tokenDto.RefreshToken || user
                .RefreshTokenExpiryTime <=  DateTime.Now)
            {
                throw new RefreshTokenBadRequest("RefreshToken is invalid");
            }
            return _jwtService.CreateToken(user, roles, false);

        }
    }
}
