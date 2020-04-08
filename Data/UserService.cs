using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebTeam6.Data;

namespace WebTeam6.Data
{
    public interface IUserService
    {
        Task<List<User>> Get();
        Task<User> Get(int id);
        Task<User> Add(User user);
        Task<User> Update(User user);
        Task<User> Delete(int id);
    }

    public class UserService : IUserService
    {
        private readonly MainContext _context;
        public UserService(MainContext context)
        {
            _context = context;
        }


        public async Task<User> Add(User user)
        {
            await _context.Database.EnsureCreatedAsync();

            user.Id = Guid.NewGuid();

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public Task<User> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<User> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
