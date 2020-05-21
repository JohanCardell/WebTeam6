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
        [Inject]
        public IGroupService GroupService { get; set; }
        [Inject]
        public IUserService UserService { get; set; }
        public List<User> GroupMembers { get; set; } = new List<User>();
        public List<User> FilteredUsers { get; set; } = new List<User>();

        [Inject]
        public AppData AppData { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            AppData.SelectedGroup = await GroupService.GetGroupById(int.Parse(Id));
            if (AppData.SelectedGroup == null) return;
            GroupMembers = AppData.SelectedGroup.Members.ToList();
            FilteredUsers = (await UserService.Get())
                .Where(x => !GroupMembers
                .Any(z => x.Id == z.Id))
                .ToList();
            FilteredUsers.Remove(AppData.SelectedGroup.Owner);
            AppData.OnChange += Update;
        }
        protected async void Update()
        {
            if (AppData.SelectedGroup == null)
            {
                AppData.OnChange -= Update;
                return;
            }
            AppData.SelectedGroup = await GroupService.GetGroupById(AppData.SelectedGroup.Id);
            GroupMembers = AppData.SelectedGroup.Members.ToList();
            FilteredUsers = (await UserService.Get())
                .Where(x => !GroupMembers
                    .Any(z => x.Id == z.Id))
                .ToList();
            FilteredUsers.RemoveAll(u => u.Id == AppData.SelectedGroup.Owner.Id);
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
        //    FilteredUsers.RemoveAll(u => u.Id == GroupObject.Owner.Id);
        //}
    }
}
