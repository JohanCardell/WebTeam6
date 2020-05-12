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
        public IEnumerable<User> userList { get; set; }
        [Inject]
        public IGroupService GroupService { get; set; }

        User userObject = new User();

        protected override async Task OnInitializedAsync()
        {
            ChildGroup = await GroupService.GetGroupById(ChildGroup.Id);
            userList = ChildGroup.Members;
        }

        protected async void DataChanged()
        {
            ChildGroup = await GroupService.GetGroupById(ChildGroup.Id);
            userList = ChildGroup.Members;
            StateHasChanged();
        }

        protected void RemoveUserFromGroup(User user)
        {
            userObject = user;
            ChildGroup.Members.Remove(userObject);
            userObject = new User();
            StateHasChanged();
        }
    }
}
