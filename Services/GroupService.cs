using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTeam6.Data;

namespace WebTeam6.Services
{
    public interface IGroupService
    {
        Task<List<Group>> Get();
        Task<Group> Get(int id);
        Task<Group> Add(Group group, int OwnerId);
        Task<Group> Update(Group group);
        Task<Group> Delete(int id);
    }

    public class GroupService: IGroupService
    {
        private readonly MainContext _context;
        public GroupService(MainContext context)
        {
            _context = context;
        }

        public async Task<Group> Add(Group group, int ownerId)
        {
            var owner = await _context.Users.FindAsync(ownerId);
            Console.WriteLine(owner);
            if(owner != null)
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

        public Task<Group> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Group>> Get()
        {
            return await _context.Groups.Include(g => g.Owner).ToListAsync();
        }

        public Task<Group> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Group> Update(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
