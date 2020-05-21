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


        public async Task<Group> Add(Group group, Task<AuthenticationState> authenticationStateTask)
        {
            await _context.Database.EnsureCreatedAsync();
            var authorizedUser = (await authenticationStateTask).User;
            var owner = await _context.Users.FirstOrDefaultAsync(u => u.UserName == authorizedUser.Identity.Name);
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
                var groupEntity = await _context.Groups.Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == group.Id);
                foreach (var id in newMembers)
                {
                    var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                    if (groupEntity.Members.Contains(userEntity) == false)
                    {
                        groupEntity.Members.Add(userEntity);
                        userEntity.Groups.Add(groupEntity);
                        Console.WriteLine($"Added {userEntity.UserName} to {groupEntity.Name}");
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
            var groupEntity = await _context.Groups
                .Include(g => g.Members)
                .Include(g => g.Events)
                .FirstOrDefaultAsync(g => g.Id == groupId);
            foreach (var e in groupEntity.Events) _context.Events.Remove(e);
            foreach (var u in groupEntity.Members) u.Groups.Remove(groupEntity);
            _context.Remove(groupEntity);
            await _context.SaveChangesAsync();
            return groupEntity;
        }

        public async Task<bool> RemoveUserFromGroup(string userId, int groupId)
        {
            var groupEntity = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            userEntity.Groups.Remove(groupEntity);
            groupEntity.Members.Remove(userEntity);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> GiveOwnership(string newOwnerId, int groupId)
        {
            var groupEntity = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
            var previousOwnerEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == groupEntity.Owner.Id);
            var newOwnerEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == newOwnerId);
            groupEntity.Owner = newOwnerEntity;
            groupEntity.Members.Add(previousOwnerEntity);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<Group>> GetGetAuthorizedUserGroups(Task<AuthenticationState> authenticationStateTask)
        {
            var authorizedUser = (await authenticationStateTask).User;
            var userEntity = await _context.Users
                .Include(u => u.Groups)
                .FirstOrDefaultAsync(u => u.UserName == authorizedUser.Identity.Name);
                          
            return userEntity.Groups.ToList();
        }
        public async Task<bool> Update(Group group)
        {
            var groupEntity = await _context.Groups.FirstOrDefaultAsync(g => g.Id == group.Id);
            _context.Entry(groupEntity).CurrentValues.SetValues(group);
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
