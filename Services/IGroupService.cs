using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTeam6.Data;

namespace WebTeam6.Services
{
    public interface IGroupService
    {
        Task<List<Group>> Get();
        Task<Group> GetGroupById(int id);
        Task<List<Group>> GetGetAuthorizedUserGroups(Task<AuthenticationState> authenticationStateTask);
        Task<Group> Add(Group group, Task<AuthenticationState> authenticationStateTask);
        Task<IEnumerable<string>> AddMembers(IEnumerable<string> newMembers, Group group);
        Task<bool> GiveOwnership(string newOwnerId, int groupId);
        Task<bool> Update(Group group);
        Task<Group> Delete(int id);
    }
}
