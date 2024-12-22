using Application.Common.Exceptions;
using Application.Dtos.UserDto;
using Application.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;   
        }
        public async Task<UserResponseDto> GetUserById(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user is null)
            {
                throw new BadRequestException("User not found!");
            }
            return _mapper.Map<UserResponseDto>(user);
        }
    }
}
