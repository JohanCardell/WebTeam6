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

        public async Task<Group> Add(Group newGroup, Task<AuthenticationState> authenticationStateTask)
        {
            var authorizedUser = (await authenticationStateTask).User;
            var ownerEntity = await _context.Users.FirstOrDefaultAsync(u => u.UserName == authorizedUser.Identity.Name);
            if (ownerEntity != null)
            {
                newGroup.Owner = ownerEntity;
                //UserGroup newUserGroupRelation = new UserGroup { //Add user as member
                //    Group = newGroup,
                //    User = ownerEntity
                //};
                ////groupEntity.Members.Add(new UserGroup{Group = groupEntity, User = ownerEntity }); // Kanske även IDn
                //await _context.UserGroups.AddAsync(newUserGroupRelation);
                await _context.Groups.AddAsync(newGroup);
                await _context.SaveChangesAsync();
                return newGroup;
            }
            Console.WriteLine("was null");
            return null;
        }

        public async Task<IEnumerable<string>> AddMembers(IEnumerable<string> newMembers, int groupId)
        {
            if (newMembers != null)
            {
                var groupEntity = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
                foreach (var id in newMembers)
                {
                    IList<UserGroup> existingGroupMemberRelations = await _context.UserGroups
                        .Where(ug => ug.UserId == id)
                        .Where(ug => ug.GroupId == groupId)
                        .ToListAsync();

                    if (existingGroupMemberRelations.Count() == 0)
                    {
                        var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                        UserGroup newUserGroupRelation = new UserGroup
                        {
                            Group = groupEntity,
                            User = userEntity
                        };
                        _context.UserGroups.Add(newUserGroupRelation);
                        Console.WriteLine($"Added {userEntity.UserName} to {groupEntity.Name}");
                    }
                }
                await _context.SaveChangesAsync();
                Console.WriteLine("context saved");
                return newMembers;
            }
            return null;
        }

        public async Task<User> AddMember(string userId, int groupId)
        {
            
                var groupEntity = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
              
                    IList<UserGroup> existingGroupMemberRelations = await _context.UserGroups
                        .Where(ug => ug.UserId == userId)
                        .Where(ug => ug.GroupId == groupId)
                        .ToListAsync();

                    if (existingGroupMemberRelations.Count() == 0)
                    {
                        var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                        UserGroup newUserGroupRelation = new UserGroup
                        {
                            Group = groupEntity,
                            User = userEntity
                        };
                        _context.UserGroups.Add(newUserGroupRelation);
                        Console.WriteLine($"Added {userEntity.UserName} to {groupEntity.Name}");
                await _context.SaveChangesAsync();
                Console.WriteLine("context saved");
                return userEntity;
            }
            return null;
        }

        public async Task<List<User>> GetGroupMembers(int groupId)
        {
            var groupEntity = await _context.Groups
                .Include(g => g.Members)
                .ThenInclude(ug => ug.User)
                .FirstOrDefaultAsync(g => g.Id == groupId);
            var members = new List<User>();
            if (groupEntity != null)
            {
                foreach (var ug in groupEntity.Members)
                {
                    members.Add(ug.User);
                }
            }
            return members;
        }

        public async Task<List<Group>> Get()
        {
            //return await _context.Groups.Include(g => g.Owner).ToListAsync();
            return await _context.Groups.ToListAsync();
        }

        public async Task<Group> GetGroupById(int groupId)
        {
            var groupEntity = await _context.Groups
                .Include(g => g.Owner)
                .Include(g => g.Events)
                .FirstOrDefaultAsync(g => g.Id == groupId);
            return groupEntity;
        }

        public async Task<Group> Delete(int groupId)
        {
            var groupEntity = await _context.Groups
                .Include(g => g.Members)
                .Include(g => g.Events)
                .Include(g => g.Messages)
                .FirstOrDefaultAsync(g => g.Id == groupId);
            foreach (var e in groupEntity.Events) _context.Events.Remove(e);
            foreach (var ug in groupEntity.Members) _context.UserGroups.Remove(ug);
            foreach (var m in groupEntity.Messages) _context.Messages.Remove(m);
            _context.Groups.Remove(groupEntity);
            await _context.SaveChangesAsync();
            return groupEntity;
        }

        public async Task<bool> RemoveMember(string userId, int groupId)
        {
            var userGroup = await _context.UserGroups
                        .FirstOrDefaultAsync(ug => ug.UserId == userId && ug.GroupId == groupId);
            _context.UserGroups.Remove(userGroup);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> GiveOwnership(string newOwnerId, int groupId)
        {
            var groupEntity = await _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
            //var previousOwnerEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == groupEntity.Owner.Id);
            var newOwnerEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == newOwnerId);
            groupEntity.Owner = newOwnerEntity;
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<Group>> GetGetAuthorizedUserGroups(Task<AuthenticationState> authenticationStateTask)
        {
            var authorizedUser = (await authenticationStateTask).User;
            var userEntity = await _context.Users
                .Include(u => u.GroupsAsMember)
                    .ThenInclude(ug => ug.Group)
                .FirstOrDefaultAsync(u => u.UserName == authorizedUser.Identity.Name);
            var userGroups = new List<Group>();
            if(userEntity?.GroupsAsMember != null)
            {
                foreach (var ug in userEntity.GroupsAsMember)
                {
                    userGroups.Add(ug.Group);
                }
            }
            return userGroups;
        }
        public async Task<bool> Update(Group group)
        {
            var groupEntity = await _context.Groups.FirstOrDefaultAsync(g => g.Id == group.Id);
            _context.Entry(groupEntity).CurrentValues.SetValues(group);
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
