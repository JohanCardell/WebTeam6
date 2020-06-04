using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTeam6.Data;

namespace WebTeam6.Services
{
    public interface IGroupService
    {
        Task<Group> Add(Group group, Task<AuthenticationState> authenticationStateTask);
        Task<List<Group>> Get();
        Task<Group> GetGroupById(int id);
        Task<List<User>> GetGroupMembers(int groupId);
        Task<List<Group>> GetGetAuthorizedUserGroups(Task<AuthenticationState> authenticationStateTask);
        Task<User> AddMember(string userId, int groupId);
        Task<IEnumerable<string>> AddMembers(IEnumerable<string> newMembers, int groupId);
        Task<bool> RemoveMember(string userId, int groupId);
        Task<bool> GiveOwnership(string newOwnerId, int groupId);
        Task<bool> Update(Group group);
        Task<Group> Delete(int id);
    }
}
