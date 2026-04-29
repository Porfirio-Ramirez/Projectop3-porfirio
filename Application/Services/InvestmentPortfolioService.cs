

using Microsoft.EntityFrameworkCore;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.InvestmentAsset;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.InvestmentPortfolio;
using ProyectoDeAprendizajeP3.Core.Application.Dtos.User;
using ProyectoDeAprendizajeP3.Core.Application.Interfaces;
using ProyectoDeAprendizajeP3.Core.Domain.Entities;
using ProyectoDeAprendizajeP3.Core.Domain.Interfaces;

namespace ProyectoDeAprendizajeP3.Core.Application.Services
{
    public class InvestmentPortfolioService : IInvestmentPortfolioService
    {
        private readonly IInvestmentPortfolioRepository _portfolioRepository;

        public InvestmentPortfolioService(IInvestmentPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<bool> AddAsync(InvestmentPortfolioDto dto)
        {
            try
            {
                InvestmentPortfolio entity = new()
                {
                    Id = 0,
                    name = dto.name,
                    description = dto.description,
                    UserId = dto.UserId
                };

                InvestmentPortfolio? returnEntity = await _portfolioRepository.AddAsync(entity);
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
                await _portfolioRepository.DeleteAsync(id);
                return true;
            }catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<InvestmentPortfolioDto>> GetAll()
        {

            try
            {
                var entities = await _portfolioRepository.GetAllList();

                var entitydto = entities.Select(a =>
                new InvestmentPortfolioDto()
                {
                    Id = a.Id,
                    name = a.name,
                    description = a.description,
                    UserId = a.UserId
                }).ToList();
                return entitydto;

            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<List<InvestmentPortfolioDto>> GetAllWithInclude()
        {
            try
            {
                var entity = _portfolioRepository.GetAllQueryWithInclude(["user"]);

                var entitiesdto = await entity.Select(a =>
                new InvestmentPortfolioDto()
                {
                    Id = a.Id,
                    name = a.name,
                    description = a.description,
                    UserId = a.UserId,
                    user = a.user == null ? null : new UserDto
                    {
                        Id = a.user.Id,
                        Name = a.user.Name,
                        Email = a.user.Email,
                        LastName = a.user.LastName,
                        Phone = a.user.Phone,
                        Role = a.user.Role,
                        ProfileImage = a.user.ProfileImage,
                        UserName = a.user.UserName
                        
                        

                    }
                }).ToListAsync();
                return entitiesdto;

            }catch (Exception)
            {
                return [];
            }
        }

        public async Task<InvestmentPortfolioDto?> GetById(int id)
        {
            
            try
            {
                var entity = _portfolioRepository.GetAllQueryWithInclude(["user"]);

                var entities = await entity.FirstOrDefaultAsync(a => a.Id == id);

                if (entities == null)
                {
                    return null;
                }

                var dto = new InvestmentPortfolioDto()
                {
                    Id = entities.Id,
                    name = entities.name,
                    description = entities.description,
                    UserId = entities.UserId,
                    user = entities.user == null ? null : new UserDto()
                    {
                        Id = entities.user.Id,
                        Name = entities.user.Name,
                        LastName = entities.user.LastName,
                        Role = entities.user.Role,
                        Phone = entities.user.Phone,
                        ProfileImage = entities.user.ProfileImage,
                        Email = entities.user.Email,
                        UserName = entities.user.UserName
                        
                    }
                };
                return dto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(InvestmentPortfolioDto dto)
        {
            try
            {
                InvestmentPortfolio entity = new()
                {
                    Id = dto.Id,
                    name = dto.name,
                    description = dto.description,
                    UserId = dto.UserId
                };
                InvestmentPortfolio? returnEntity = await _portfolioRepository.UpdateAsync(entity.Id, entity);
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
