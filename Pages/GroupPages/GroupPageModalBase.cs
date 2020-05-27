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
        public IUserService UserService { get; set; }

        [Inject]
        public IGroupService GroupService { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        [Parameter]
        public Group GroupObject { get; set; }

        [Parameter]
        public Action DataChanged { get; set; }

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
            if (selectedUsers != null)
            {
                await GroupService.AddMembers(selectedUsers, GroupObject.Id);
                await CloseModal("addMemberModal");
                DataChanged?.Invoke();
            }
        }

        protected async Task AssignNewOwner()
        {
            if (newOwnerId != null)
            {
                await GroupService.GiveOwnership(newOwnerId, GroupObject.Owner.Id, GroupObject.Id);
                await CloseModal("assignOwnerModal");
                DataChanged?.Invoke();
                //NavManager.NavigateTo($"/groupdetails/{GroupObject.Id}", true);
            }
        }
    }
}
