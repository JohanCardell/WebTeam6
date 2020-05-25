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
        protected Group GroupObject = new Group();
        [Inject]
        public IGroupService GroupService { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        public List<User> GroupMembers { get; set; } = new List<User>();
        public List<User> FilteredUsers { get; set; } = new List<User>();

        [Parameter]
        public string Id { get; set; }

        //protected async override Task OnInitializedAsync()
        //{
        //    GroupObject = await GroupService.GetGroupById(int.Parse(Id));
        //    GroupMembers = GroupObject.Members.ToList();
        //    FilteredUsers = (await UserService.Get())
        //        .Where(x => !GroupMembers
        //            .Any(z => x.Id == z.Id))
        //        .ToList();
        //    GroupMembers.Remove(GroupObject.Owner);
        //    FilteredUsers.Remove(GroupObject.Owner);
        //}
        protected async void DataChanged()
        {
            GroupObject = await GroupService.GetGroupById(GroupObject.Id);
            GroupMembers = GroupObject.Members. ;
            FilteredUsers = (await UserService.Get())
                .Where(x => !GroupMembers
                    .Any(z => x.Id == z.Id))
                .ToList();
            GroupMembers.RemoveAll(u => u.Id == GroupObject.Owner.Id);
            FilteredUsers.RemoveAll(u => u.Id == GroupObject.Owner.Id);
            StateHasChanged();
        }
        protected override async Task OnParametersSetAsync()
        {
            GroupObject = await GroupService.GetGroupById(int.Parse(Id));
            GroupMembers = GroupObject.Members.ToList();
            FilteredUsers = (await UserService.Get())
                .Where(x => !GroupMembers
                    .Any(z => x.Id == z.Id))
                .ToList();
            GroupMembers.RemoveAll(u => u.Id == GroupObject.Owner.Id);
            FilteredUsers.RemoveAll(u => u.Id == GroupObject.Owner.Id);
        }
    }
}
