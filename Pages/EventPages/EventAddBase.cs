using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using WebTeam6.Data;
using WebTeam6.Services;

namespace WebTeam6.Pages.EventPages
{
    public class EventAddBase : ComponentBase
    {
        [Inject]
        private IEventService Service { get; set; }

        [Parameter]
        public Group Group { get; set; }

        public Event Event { get; set; } = new Event { StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) };

        [Parameter]
        public Action DataChanged { get; set; }

        public bool Saved { get; set; } = false;

        public bool Editing { get; set; }

        public async void Add()
        {
            Event.Group = Group;
            var res = await Service.Add(Event);
            if (res != null) Saved = true;
            Event = new Event { StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) };
            DataChanged?.Invoke();
            base.StateHasChanged();
        }
    }
}
