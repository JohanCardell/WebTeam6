using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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
        [Inject]
        public IGroupService GroupService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> authenticationStateTask { get; set; }

        [Parameter]
        public string Id { get; set; }

        public User CurrentUser { get; set; } = new User();

        public List<User> GroupMembers { get; set; } = new List<User>();

        public Group GroupObject { get; set; } = new Group();

        protected async override Task OnInitializedAsync()
        {
            GroupObject = await GroupService.GetGroupById(int.Parse(Id));
            GroupMembers = await GroupService.GetGroupMembers(GroupObject.Id);
            GroupMembers.Remove(GroupObject.Owner);
            CurrentUser = await UserService.GetAuthorizedUser(authenticationStateTask);

        }
        protected async void DataChanged()
        {
            GroupObject = await GroupService.GetGroupById(int.Parse(Id));
            GroupMembers = await GroupService.GetGroupMembers(GroupObject.Id);
            GroupMembers.Remove(GroupObject.Owner);
            CurrentUser = await UserService.GetAuthorizedUser(authenticationStateTask);
            StateHasChanged();
        }
    }
}
