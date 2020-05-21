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

        [Inject]
        public AppData AppData { get; set; }

        public Event Event { get; set; } = new Event { StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) };

        protected override async Task OnInitializedAsync()
        {
            var res = await Service.Get(AppData.SelectedGroup);
            if (res != null) AppData.SelectedGroup.Events = res;
            AppData.OnChange += Update;
        }

        public async void Update()
        {
            var res = await Service.Get(AppData.SelectedGroup);
            if (res != null) AppData.SelectedGroup.Events = res;
            StateHasChanged();
        }

        public async void Add()
        {
            var res = await Service.Add(Event);
            if(res != null)
            {
                Event = new Event { StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) };
                Update();
            }
        }
    }
}
