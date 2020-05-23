using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTeam6.Data;

namespace WebTeam6.Services
{
    public interface IMessageService
    {
        Task<Message> Get(int id);
        Task<List<Message>> Get();
        Task<List<Message>> Get(Group g);
        Task<Message> Add(Message e);
        Task<bool> Update(Message e);
        Task<Message> Delete(int id);
    }

    public class MessageService : IMessageService
    {
        private readonly MainContext _context;

        public MessageService(MainContext context)
        {
            this._context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<Message> Add(Message e)
        {
            var g = _context.Groups.FirstOrDefault(g => g.Id == e.Group.Id);
            e.Group = g;
            var u = _context.Users.FirstOrDefault(u => u.Id == e.Creator.Id);
            e.Creator = u;
            var res = await _context.Messages.AddAsync(e);

            await _context.SaveChangesAsync();

            return res?.Entity;
        }

        public async Task<Message> Delete(int id)
        {
            var ev = await _context.Messages.FirstOrDefaultAsync(e => e.Id == id);

            if (ev != null)
            {
                _context.Messages.Remove(ev);
                await _context.SaveChangesAsync();
            }

            return ev;
        }

        public async Task<Message> Get(int id)
        {
            return await _context.Messages.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Message>> Get(Group g)
        {
            var group = await (_context.Groups.Include(g => g.Messages).FirstOrDefaultAsync(gr => gr.Id == g.Id));
            return group.Messages.ToList();
        }

        public async Task<List<Message>> Get()
        {
            return await _context.Messages.ToListAsync();
        }

        public async Task<bool> Update(Message ev)
        {
            var res = await _context.Messages.FirstOrDefaultAsync(e => e.Id == ev.Id);
            _context.Entry(res).CurrentValues.SetValues(ev);
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
