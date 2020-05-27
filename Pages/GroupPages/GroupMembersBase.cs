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

        public User CurrentUser { get; set; }

        public List<User> GroupMembers { get; set; } = new List<User>();
        
        public List<User> FilteredUsers { get; set; } = new List<User>();
              
        [Parameter]
        public Action DataChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CurrentUser = await UserService.GetAuthorizedUser(authenticationStateTask);
            GroupMembers = await GroupService.GetGroupMembers(GroupObject.Id);
            GroupMembers.Remove(GroupObject.Owner);
        }
      
        protected async Task DeleteGroup(int groupId)
        {
            await GroupService.Delete(groupId);
            NavManager.NavigateTo("/mygroups");
            StateHasChanged();
        }

        protected async Task RemoveUserFromGroup(string userId)
        {
            await GroupService.RemoveMember(userId, GroupObject.Id);
            DataChanged?.Invoke();
        }

        protected async Task FilterUsers()
        {
            FilteredUsers = (await UserService.Get())
               .Where(x => !GroupMembers
                   .Any(z => x.Id == z.Id))
               .ToList();
        }
    }
}
