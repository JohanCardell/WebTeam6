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
    public class GroupMembersBase : ComponentBase
    {
        [CascadingParameter]
        protected Task<AuthenticationState> authenticationStateTask { get; set; }
        [Parameter]
        public Group GroupObject { get; set; } = new Group();
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
        [Parameter]
        public Action DataChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CurrentUser = await UserService.GetAuthorizedUser(authenticationStateTask);
        }
        protected override Task OnParametersSetAsync()
        {
            return base.OnParametersSetAsync();
        }

        protected async Task DeleteGroup(int groupId)
        {
            await GroupService.Delete(groupId);
            NavManager.NavigateTo("/mygroups");
        }

        protected async Task RemoveUserFromGroup(User user)
        {
            user.GroupsAsMember.Remove(GroupObject);
            await UserService.Update(user);
            GroupObject.Members.Remove(user);
            await GroupService.Update(GroupObject);
            user = new User();
            DataChanged?.Invoke();
        }
    }
}
