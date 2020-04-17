using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebTeam6.Data;

namespace WebTeam6.Services
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
        // private readonly UserManager<User> _manager;
        private readonly MainContext _context;
        public UserService(MainContext context)
        {
            _context = context;
            //TODO: Add UserManager with Dependency Injection to be able to Hash Password
        }


        public async Task<User> Add(User user)
        {
            await _context.Database.EnsureCreatedAsync();

            // user.Password = _manager.PasswordHasher.HashPassword(user, user.Password);

            var exists = await _context.Users.Select(u => u).Where(e => e.Email == user.Email || e.Username == user.Username).FirstOrDefaultAsync();

            if (exists == default)
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return user;
            }
            else
            {
                return default;
            }

        }

        public async Task<User> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);

            user.Groups.ToList().ForEach(g => g.Members.Remove(user));

            _context.Groups.Where(g => g.Owner == user).ToList().ForEach(g => g.Owner = null);
            
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }
           
        public async Task<List<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> Get(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public Task<User> Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
