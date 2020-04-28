using System.Collections.Generic;
using System.Threading.Tasks;
using WebTeam6.Data;

namespace WebTeam6.Services
{
    public interface IGroupService
    {
        Task<List<Group>> Get();
        Task<Group> GetGroupById(int id);
        Task<Group> Add(Group group, string ownerName);
        Task<Group> Update(Group group);
        Task<Group> Delete(int id);
    }
}
