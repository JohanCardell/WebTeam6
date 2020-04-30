using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTeam6.Data;
using WebTeam6.Services;

namespace WebTeam6.Pages.EventPages
{
    public class EventListBase : ComponentBase
    {
        [Inject]
        private IEventService Service { get; set; }
        public List<Event> Events { get; set; }
        public Event NewEvent { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Events = (await Service.Get()).ToList();
        }

        public async Task<Event> Create(Event e)
        {
            return await Service.Add(e);
        }
    }
}
