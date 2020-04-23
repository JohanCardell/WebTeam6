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
        Task<Group> Add(Group group, string ownerName);
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


        public async Task<Group> Add(Group group, string name)
        {
            await _context.Database.EnsureCreatedAsync();
            var ownerName = await _context.Users.FirstAsync(n => n.UserName == name);
            var owner = await _context.Users.FindAsync(ownerName.Id);
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

        //public async Task<Group> Add(Group group, string name)
        //{
        //    var ownerId = await _context.Users.FirstAsync(n => n.UserName == name);
        //    var owner = await _context.Users.FindAsync(ownerId);
        //    Console.WriteLine(owner);
        //    if(owner != null)
        //    {
        //        Console.WriteLine("was not null");
        //        group.Owner = owner;
        //        await _context.Groups.AddAsync(group);
        //        owner.Groups.Add(group);
        //        await _context.SaveChangesAsync();
        //        return group;
        //    }
        //    Console.WriteLine("was null");
        //    return null;
        //}

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
            //return await _context.Groups.ToListAsync();
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
