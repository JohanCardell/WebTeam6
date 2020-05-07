using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebTeam6.Data;

namespace WebTeam6.Services
{

    public class UserService : IUserService
    {
        private readonly UserManager<User> _manager;
        private readonly MainContext _context;
        public UserService(MainContext context, UserManager<User> manager)
        {
            _context = context;
            _manager = manager;
        }


        public async Task<User> Add(User user)
        {
            await _context.Database.EnsureCreatedAsync();

            user.PasswordHash = _manager.PasswordHasher.HashPassword(user, user.PasswordHash);

            var exists = await _context.Users.Select(u => u).Where(e => e.Email == user.Email || e.UserName == user.UserName).FirstOrDefaultAsync();

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

        public async Task<User> Delete(string id)
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

        public async Task<User> Get(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetAuthorizedUser(Task<AuthenticationState> authenticationStateTask)
        {
            var loggedInUser = (await authenticationStateTask).User;
            var user = await _context.Users.FirstAsync(u => u.UserName == loggedInUser.Identity.Name);
            return user;
        }

        public Task<User> Update(User user)
        {
            throw new NotImplementedException();
        }

        //public async Task<User> UpdateUserGroups (User user, Group group)
        //{
        //     _context.Update(user);
        //     await _context.SaveChangesAsync();
        //     return user;
        //}
    }
}
