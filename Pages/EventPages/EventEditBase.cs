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
    public class EventEditBase : ComponentBase
    {
        [Inject]
        private IEventService Service { get; set; }
        
        [Parameter]
        public Event Event { get; set; } = new Event();

        [Parameter]
        public EventCallback<Event> EventChanged { get; set; }

        //[Parameter]
        //public EventCallback<bool> EditingChanged { get; set; }

        public bool Saved { get; set; } = false;
        public bool Editing { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Event = await Service.Get(Event.Id);
        }

        public async void Update()
        {
            Saved = await Service.Update(Event);
            base.StateHasChanged();
        }

        public Task OnEventChanged(ChangeEventArgs e)
        {
            return EventChanged.InvokeAsync(Event);
        }

        //public Task OnEditingChanged(ChangeEventArgs e)
        //{
        //    return EditingChanged.InvokeAsync(Editing);
        //}
    }
}
