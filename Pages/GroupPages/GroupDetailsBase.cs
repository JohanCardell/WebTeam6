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
        protected Group GroupObject = new Group();
        [Inject]
        public IGroupService GroupService { get; set; }
        public IEnumerable<User> UserList { get; set; } = new List<User>();
        protected User userObject = new User();

        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            GroupObject = await GroupService.GetGroupById(int.Parse(Id));
        }
        protected async void DataChanged()
        {
            await GroupService.GetGroupById(GroupObject.Id);
            UserList = GroupObject.Members;
            StateHasChanged();
        }
        protected override async Task OnParametersSetAsync()
        {
            Id = Id ?? "1";
            GroupObject = await GroupService.GetGroupById(int.Parse(Id));
        }
    }
}
