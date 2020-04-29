using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTeam6.Data;

namespace WebTeam6.Services
{
    public interface IEventService
    {
        Task<Event> Get(int id);
        Task<List<Event>> Get();
        Task<List<Event>> GetForGroup(Group g);
        Task<Event> Add(Event e);

        Task<Event> Update(Event e);

        Task<Event> Delete(int id);
    }

    public class EventService : IEventService
    {
        private readonly MainContext _context;

        public EventService(MainContext context)
        {
            this._context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<Event> Add(Event e)
        {
            var res = await _context.Events.AddAsync(e);

            await _context.SaveChangesAsync();

            return res.Entity;
        }

        public Task<Event> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Event> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Event>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<List<Event>> GetForGroup(Group g)
        {
            throw new NotImplementedException();
        }

        public Task<Event> Update(Event e)
        {
            throw new NotImplementedException();
        }
    }
}
