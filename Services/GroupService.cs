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

        public async Task<IEnumerable<string>> AddMembers(IEnumerable<string> newMembers, Group group)
        {
            if (newMembers != null)
            {
                var actualGroup = _context.Groups.FirstOrDefault(g => g.Id == group.Id);
                foreach (var id in newMembers)
                {
                    var user = await _context.Users
                        .Where(u => u.Id == id)
                        .FirstOrDefaultAsync();
                    if (actualGroup.Members.Contains(user) == false)
                    {
                        actualGroup.Members.Add(user);
                        Console.WriteLine($"Added {user.UserName} to {actualGroup.Name}");
                    }
                }
                await _context.SaveChangesAsync();
                Console.WriteLine("context saved");
                return newMembers;
            }
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

        public async Task<Group> GetGroupById(int id)
        {
            return await _context.Groups
                .Include(g => g.Owner)
                .Include(g => g.Members)
                .Where(g => g.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<Group> Update(Group group)
        {
            throw new NotImplementedException();
        }
    }
}
