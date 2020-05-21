using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTeam6.Data;
using WebTeam6.Services;

namespace WebTeam6.Pages.EventPages
{
    public class EventDetailsBase : ComponentBase
    {
        [Inject]
        private IEventService Service { get; set; }

        [Parameter]
        public Action DataChanged { get; set; }

        [Parameter]
        public Event Event { get; set; }
        public bool Editing { get; set; }

        public async void Update()
        {
            if (Event != null)
            {
                var saved = await Service.Update(Event);
                if (saved) Editing = false;
            }
            DataChanged?.Invoke();
        }
        public async void Delete()
        {
            if (Event != null)
            {
                await Service.Delete(Event.Id);
                DataChanged?.Invoke();
            }
        }
    }
}
