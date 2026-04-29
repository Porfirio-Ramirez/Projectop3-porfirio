
using Microsoft.EntityFrameworkCore;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.User;
using ProyectoDeAprendizajeP3.Core.Application.Helpers;
using ProyectoDeAprendizajeP3.Core.Application.Interfaces;
using ProyectoDeAprendizajeP3.Core.Domain.Entities;
using ProyectoDeAprendizajeP3.Core.Domain.Interfaces;

namespace ProyectoDeAprendizajeP3.Core.Application.Services
{
    
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> LoginAsync(LoginDto dto)
        {
            User? user = await _userRepository.LoginAsync(dto.UserName, dto.Password);
            if (user == null)
            {
                return null;
            }
            UserDto dtos = new()
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Phone = user.Phone,
                ProfileImage = user.ProfileImage,
                Role = user.Role
            };
            return null;
        }
        public async Task<bool> AddAsync(SaveUserDto dto)
        {

            try
            {
                User entity = new()
                {
                    Id = 0,
                    Name = dto.Name,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Password = PasswordEncryptation.Computesha256Hash(dto.Password),
                    UserName = dto.UserName,
                    ProfileImage = dto.ProfileImage,
                    Role = dto.Role

                };

                User? returnEntity = await _userRepository.AddAsync(entity);
                if (returnEntity == null)
                {
                    return false;
                }
                return true;
            }catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _userRepository.DeleteAsync(id);
                return true;
            }catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<UserDto>> GetAll()
        {
            try
            {
                var entity = await _userRepository.GetAllList();

                var entitiedto = entity.Select(a =>
                new UserDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    LastName = a.LastName,
                    Email = a.Email,
                    Phone = a.Phone,
                    ProfileImage = a.ProfileImage,
                    Role = a.Role,
                    UserName = a.UserName
                }).ToList();
                return entitiedto;

            }catch (Exception)
            {
                return [];
            }
          

        }

        public async Task<List<UserDto>> GetAllWithInclude()
        {
            try
            {
                var entity = _userRepository.GetAllQueryWithInclude(["investmentPortfolios"]);

                var entitydto = await entity.Select(a =>
                new UserDto()
                {
                    Id = a.Id,
                    Name = a.Name,
                    LastName = a.LastName,
                    Email = a.Email,
                    Phone = a.Phone,
                    ProfileImage = a.ProfileImage,
                    Role = a.Role,
                    UserName = a.UserName
                }).ToListAsync();
                return entitydto;
            }catch (Exception)
            {
                return [];
            }
            
        }

        public async Task<UserDto?> GetById(int id)
        {
            try
            {
                var entity = await _userRepository.GetById(id);

                if (entity == null)
                {
                    return null;
                }


                UserDto dto = new()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    LastName = entity.LastName,
                    Email = entity.Email,
                    Role = entity.Role,
                    ProfileImage = entity.ProfileImage,
                    Phone = entity.Phone,
                    UserName = entity.UserName
                };

                return dto;

            }catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(SaveUserDto dto)
        {
            try
            {
                var entitydb = await _userRepository.GetById(dto.Id);

                if (entitydb == null)
                {
                    return false;
                }

                User entity = new()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Password = string.IsNullOrWhiteSpace(dto.Password) ? entitydb.Password : PasswordEncryptation.Computesha256Hash(dto.Password),
                    Phone = dto.Phone,
                    ProfileImage = dto.ProfileImage,
                    Role = dto.Role,
                    UserName = dto.UserName
                };
                User? returnEntity = await _userRepository.UpdateAsync(entity.Id, entity);
                if (returnEntity == null)
                {
                    return false;
                }
                return true;
            }catch (Exception)
            {
                return false;
            }
        }
    }
}
