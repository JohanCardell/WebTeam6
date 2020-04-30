using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTeam6.Data;

namespace WebTeam6.Pages.EventPages
{
    public class EventDetailsBase : ComponentBase
    {
        [Parameter]
        public Event Event { get; set; }
    }
}
