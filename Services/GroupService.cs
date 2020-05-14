using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
                group.Members.Add(owner);
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
                var targetGroup = await _context.Groups.Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == group.Id);
                foreach (var id in newMembers)
                {
                    var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                    if (targetGroup.Members.Contains(user) == false)
                    {
                        targetGroup.Members.Add(user);
                        user.Groups.Add(targetGroup);
                        Console.WriteLine($"Added {user.UserName} to {targetGroup.Name}");
                    }
                }
                await _context.SaveChangesAsync();
                Console.WriteLine("context saved");
                return newMembers;
            }
            return null;
        }

        public async Task<List<Group>> Get()
        {
            return await _context.Groups.Include(g => g.Owner).ToListAsync();
        }

        public async Task<Group> GetGroupById(int groupId)
        {
            return await _context.Groups
                .Include(g => g.Owner)
                .Include(g => g.Members)
                .Include(g => g.Events)
                .Where(g => g.Id == groupId)
                .FirstOrDefaultAsync();
        }

        public async Task<Group> Delete(int groupId)
        {
            var targetGroup = await _context.Groups
                .Include(g => g.Members)
                .FirstOrDefaultAsync(g => g.Id == groupId);
            foreach (User u in targetGroup.Members) u.Groups.Remove(targetGroup);
            _context.Remove(targetGroup);
            await _context.SaveChangesAsync();
            return targetGroup;
        }

        public async Task<bool> RemoveUserFromGroup(string userId, int groupId)
        {
            var targetGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
            var targetUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            targetUser.Groups.Remove(targetGroup);
            targetGroup.Members.Remove(targetUser);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> GiveOwnership(string newOwnerId, int groupId)
        {
            var newOwner = await _context.Users.FirstOrDefaultAsync(u => u.Id == newOwnerId);
            var targetGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
            targetGroup.Owner = newOwner;

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<Group>> GetGetAuthorizedUserGroups(Task<AuthenticationState> authenticationStateTask)
        {
            var authorizedUser = (await authenticationStateTask).User;
            var user = await _context.Users
                .Include(u => u.Groups)
                .FirstAsync(u => u.UserName == authorizedUser.Identity.Name);
                          
            return user.Groups.ToList();
        }
        public async Task<bool> Update(Group group)
        {
            var targetGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Id == group.Id);
            _context.Entry(targetGroup).CurrentValues.SetValues(group);
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
