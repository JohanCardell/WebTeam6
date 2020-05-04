using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTeam6.Data;
using WebTeam6.Services;

namespace WebTeam6.Pages.GroupPages
{
    public class GroupMembersViewBase: ComponentBase
    {
        [Parameter]
        public Group ChildGroup { get; set; }

        //public IEnumerable<User> userList { get; set; } = new List<User>();
        public IUserService UserService { get; set; }

        User userObject = new User();

        //protected override async Task OnInitializedAsync()
        //{
        //    userList =  ChildGroup.Members;

        //}

        //private void InitializeUserObject()
        //{
        //    userObject = new User();
        //}

        //private async void DataChanged()
        //{
        //    userList = await service.Get();
        //    StateHasChanged();
        //}

        protected void RemoveUserFromGroup(User user)
        {
            userObject = user;
            ChildGroup.Members.Remove(userObject);
            userObject = new User();
            StateHasChanged();
        }
    }
}
