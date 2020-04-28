using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTeam6.Data;
using WebTeam6.Services;

namespace WebTeam6.Pages.GroupPages
{
    public class GroupDetailsBase : ComponentBase
    {
        public Group Group { get; set; } = new Group();
        [Inject]
        public IGroupService GroupService { get; set; }

        [Parameter]
        public int Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Group = await GroupService.GetGroupById(Id);
        }
    }
}
