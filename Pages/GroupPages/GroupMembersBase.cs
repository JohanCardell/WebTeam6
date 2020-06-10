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

        [Inject]
        public IGroupService GroupService { get; set; }

        [Inject]
        public IUserService UserService { get; set; }

        [Inject]
        NavigationManager NavManager { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> authenticationStateTask { get; set; }

        [Parameter]
        public Group GroupObject { get; set; }

        [Parameter]
        public User CurrentUser { get; set; } = new User();

        [Parameter]
        public List<User> GroupMembers { get; set; } = new List<User>();
        
        [Parameter]
        public Action DataChanged { get; set; }

        public List<User> FilteredUsers { get; set; } = new List<User>();

        protected async Task DeleteGroup(int groupId)
        {
            await GroupService.Delete(groupId);
            NavManager.NavigateTo("/mygroups", true);
        }

        protected async Task LeaveGroup(string userId)
        {
            await GroupService.RemoveMember(userId, GroupObject.Id);
            NavManager.NavigateTo("/mygroups", true);
        }

        protected async Task FilterUsers()
        {
            FilteredUsers = (await UserService.Get())
               .Where(x => !GroupMembers
                   .Any(z => x.Id == z.Id))
               .Where(x => x.Id != GroupObject.OwnerId)
               .ToList();
        }

        protected async void RefreshMembersList()
        {
            GroupObject = await GroupService.GetGroupById(GroupObject.Id);
            GroupMembers = await GroupService.GetGroupMembers(GroupObject.Id);
            GroupMembers.Remove(GroupObject.Owner);
            CurrentUser = await UserService.GetAuthorizedUser(authenticationStateTask);
            StateHasChanged();
        }

        protected async Task RemoveUserFromGroup(string userId)
        {
            await GroupService.RemoveMember(userId, GroupObject.Id);
            RefreshMembersList();
        }
    }
}
