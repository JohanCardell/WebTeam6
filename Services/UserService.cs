using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public UserService(MainContext context, UserManager<User> manager, IMapper mapper)
        {
            _context = context;
            _manager = manager;
            _mapper = mapper;
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
            var userEntity = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            foreach (var ug in userEntity.GroupsAsMember) _context.UserGroups.Remove(ug);
            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
            return userEntity;
        }

        public async Task<List<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> Get(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetAuthorizedUser(Task<AuthenticationState> authenticationStateTask)
        {
            var authorizedUser = (await authenticationStateTask).User;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == authorizedUser.Identity.Name);
            return user;
        }

        public async Task<bool> Update(User user)
        {
            var targetUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            _context.Entry(targetUser).CurrentValues.SetValues(user);
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
