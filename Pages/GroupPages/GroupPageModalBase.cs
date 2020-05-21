using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using WebTeam6.Data;
using WebTeam6.Services;

namespace WebTeam6.Pages.GroupPages
{
    public class GroupPageModalBase: ComponentBase
    {
        [Inject]
        public IJSRuntime jSRuntime { get; set; }

        [Inject]
        public IGroupService GroupService { get; set; }

        [Inject]
        public AppData AppData { get; set; }

        [Parameter]
        public List<User> GroupMembers { get; set; } = new List<User>();

        [Parameter]
        public List<User> FilteredUsers { get; set; } = new List<User>();
        protected IEnumerable<string> selectedUsers = new string[] { "", "" };
        protected string newOwnerId = string.Empty;


        protected async Task CloseModal(string modalId)
        {
            await jSRuntime.InvokeAsync<object>("CloseModal", modalId);
        }
        protected async Task AddSelectedUsers()
        {
            await GroupService.AddMembers(selectedUsers, AppData.SelectedGroup);
            AppData.SelectedGroup = await GroupService.GetGroupById(AppData.SelectedGroup.Id);
            await CloseModal("addMemberModal");
        }

        protected async Task AssignNewOwner()
        {
            await GroupService.GiveOwnership(newOwnerId, AppData.SelectedGroup.Id);
            AppData.SelectedGroup = await GroupService.GetGroupById(AppData.SelectedGroup.Id);
            await CloseModal("assignOwnerModal");
        }
    }
}
