using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WebTeam6.Data;
using WebTeam6.Services;

namespace WebTeam6.Pages.GroupPages
{
    public class GroupDetailsBase : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IGroupService GroupService { get; set; }

        //public List<User> GroupMembers { get; set; } = new List<User>();

        public Group GroupObject { get; set; }

        protected async override Task OnInitializedAsync()
        {
            GroupObject = await GroupService.GetGroupById(int.Parse(Id));
            //GroupMembers.RemoveAll(u => u.Id == GroupObject.Owner.Id);
            //FilteredUsers.RemoveAll(u => u.Id == GroupObject.Owner.Id);
        }
        protected async void DataChanged()
        {
            GroupObject = await GroupService.GetGroupById(int.Parse(Id));
            //GroupMembers = await GroupService.GetGroupMembers(GroupObject.Id);
            StateHasChanged();
        }
        //protected override async Task OnParametersSetAsync()
        //{
        //    GroupObject = await GroupService.GetGroupById(int.Parse(Id));
        //    GroupMembers = GroupObject.Members.ToList();
        //    FilteredUsers = (await UserService.Get())
        //        .Where(x => !GroupMembers
        //            .Any(z => x.Id == z.Id))
        //        .ToList();
        //    GroupMembers.RemoveAll(u => u.Id == GroupObject.Owner.Id);
        //    FilteredUsers.RemoveAll(u => u.Id == GroupObject.Owner.Id);
        //}
    }
}
