using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTeam6.Data;
using WebTeam6.Services;


namespace WebTeam6.Pages.GroupPages
{
    public class GroupMembersBase : ComponentBase
    {
        [CascadingParameter]
        protected Task<AuthenticationState> authenticationStateTask { get; set; }

        public User CurrentUser { get; set; } = new User();

        [Parameter]
        public List<User> GroupMembers { get; set; } = new List<User>();

        [Parameter]
        public List<User> FilteredUsers { get; set; } = new List<User>();

        [Inject]
        public IGroupService GroupService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        NavigationManager NavManager { get; set; }

        [Inject]
        public AppData AppData { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CurrentUser = await UserService.GetAuthorizedUser(authenticationStateTask);
        }

        protected async Task DeleteGroup(int groupId)
        {
            NavManager.NavigateTo("/");
            AppData.Groups.Remove(AppData.SelectedGroup);
            await GroupService.Delete(groupId);
        }

        protected async Task RemoveUserFromGroup(User user)
        {
            user.Groups.Remove(AppData.SelectedGroup);
            await UserService.Update(user);
            AppData.SelectedGroup.Members.Remove(user);
            await GroupService.Update(AppData.SelectedGroup);
            user = new User();
        }
    }
}
