using Microsoft.EntityFrameworkCore;
using ProyectoDeAprendizajeP3.Core.Application.Helpers;
using ProyectoDeAprendizajeP3.Core.Domain.Entities;
using ProyectoDeAprendizajeP3.Core.Domain.Interfaces;
using ProyectoDeAprendizajeP3.Infrastruture.Persistence.Contexts;

namespace ProyectoDeAprendizajeP3.Infrastruture.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly InvestmentContext _context;
        public UserRepository(InvestmentContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> LoginAsync(string userName, string password)
        {
            string passwordEncrypt = PasswordEncryptation.Computesha256Hash(password);

            User? user = await _context.Set<User>().FirstOrDefaultAsync
            (u => u.UserName == userName && u.Password == passwordEncrypt);
            return user;
        }
    }
}
