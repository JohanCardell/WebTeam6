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

        [Parameter]
        public Group Group { get; set; }

        [Parameter]
        public IEnumerable<Event> Events { get; set; }
        public Event Event { get; set; } = new Event { StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) };

        protected async void DataChanged()
        {
            var res = await Service.Get(Group);
            if(res != null) Events = res;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Events = Group.Events;
        }
        public async void Add()
        {
            Event.Group = Group;
            var res = await Service.Add(Event);
            if(res != null)
            {
                Event = new Event { StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) };
                DataChanged();
                base.StateHasChanged();
            }
        }
        protected async void Delete(Event Event)
        {
            if (Event != null)
            {
                await Service.Delete(Event.Id);
                DataChanged();
                base.StateHasChanged();
            }
        }
    }
}
