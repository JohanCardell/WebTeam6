using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTeam6.Data;
using WebTeam6.Services;


namespace WebTeam6.Pages.GroupPages
{
    public class GroupMembersBase: ComponentBase
    {
        [CascadingParameter]
        protected Task<AuthenticationState> authenticationStateTask { get; set; }
        [Parameter]
        public Group GroupObject { get; set; } = new Group();
        public User CurrentUser { get; set; } = new User();
        [Parameter]
        public IEnumerable<User> UserList { get; set; }
        [Inject]
        public IGroupService GroupService { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        [Parameter]
        public Action DataChanged { get; set; }

        protected User userObject = new User();

        protected override async Task OnInitializedAsync()
        {
            await GroupService.GetGroupById(GroupObject.Id);
            CurrentUser = await UserService.GetAuthorizedUser(authenticationStateTask);
            UserList = GroupObject.Members;
        }

        //protected async void DataChanged()
        //{
        //    await GroupService.GetGroupById(GroupObject.Id);
        //    UserList = GroupObject.Members;
        //    StateHasChanged();
        //}

        protected void RemoveUserFromGroup(User user)
        {
            userObject = user;
            GroupObject.Members.Remove(userObject);
            userObject = new User();
            StateHasChanged();
        }
    }
}
