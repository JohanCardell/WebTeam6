using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTeam6.Data
{
    public interface IGroupService
    {
        Task<List<Group>> Get();
        Task<Group> Get(string id);
        Task<Group> Add(Group group);
        Task<Group> Update(Group group);
        Task<Group> Delete(string id);
    }

    public class GroupService: IGroupService
    {
        private readonly MainContext _context;
        public GroupService(MainContext context)
        {
            _context = context;
        }

        public async Task<Group> Add(Group group)
        {
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public Task<Group> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Group>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Group> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Group> Update(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
