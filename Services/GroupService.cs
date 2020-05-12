using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTeam6.Data;

namespace WebTeam6.Services
{

    public class GroupService: IGroupService
    {
        private readonly MainContext _context;
        public GroupService(MainContext context)
        {
            _context = context;
        }


        public async Task<Group> Add(Group group)
        {
            await _context.Database.EnsureCreatedAsync();
            var owner = await _context.Users.FirstAsync(o => o.UserName == group.Owner.UserName);
            Console.WriteLine(owner);
            if (owner != null)
            {
                Console.WriteLine("was not null");
                group.Owner = owner;
                await _context.Groups.AddAsync(group);
                owner.Groups.Add(group);
                await _context.SaveChangesAsync();
                return group;
            }
            Console.WriteLine("was null");
            return null;
        }

        public async Task<IEnumerable<string>> AddMembers(IEnumerable<string> newMembers, Group group)
        {
            
            if (newMembers != null)
            {
                var actualGroup = _context.Groups.FirstOrDefault(g => g.Id == group.Id);
                foreach (var name in newMembers)
                {
                    var user = await _context.Users.FirstAsync(u => u.Id == name);
                    if (user != null)
                    {
                        actualGroup.Members.Add(user);
                        user.Groups.Add(actualGroup);
                        Console.WriteLine($"Added {user.UserName} to {actualGroup.Name}");
                    }
                }
                await _context.SaveChangesAsync();
                Console.WriteLine("context saved");
                return newMembers;
            }
            return null;
        }

        public async Task<Group> Delete(int id)
        {
            var group = await _context.Groups.FindAsync(id);

            group.Members.ToList().ForEach(g => g.Groups.Remove(group));
            _context.Remove(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<List<Group>> Get()
        {
            return await _context.Groups.Include(g => g.Owner).ToListAsync();
        }

        public async Task<Group> GetGroupById(int id)
        {
            return await _context.Groups
                .Include(g => g.Owner)
                .Include(g => g.Members)
                .Include(g => g.Events)
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<List<Group>> GetGetAuthorizedUserGroups(Task<AuthenticationState> authenticationStateTask)
        {
            var authorizedUser = (await authenticationStateTask).User;
            var user = await _context.Users
                .Include(u => u.Groups)
                .ThenInclude(g => g.Members)
                .FirstAsync(u => u.UserName == authorizedUser.Identity.Name);
                          
            return user.Groups.ToList();
        }
        public async Task<bool> Update(Group group)
        {
            var res = await _context.Groups.FirstOrDefaultAsync(g => g.Id == group.Id);
            _context.Entry(res).CurrentValues.SetValues(group);
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
