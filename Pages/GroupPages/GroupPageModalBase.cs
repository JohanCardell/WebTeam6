using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IUserService UserService { get; set; }
        [Parameter]
        public Group GroupObject { get; set; }
        [Parameter]
        public Action DataChanged { get; set; }
        [Parameter]
        public List<User> Users { get; set; } = new List<User>();

        protected IEnumerable<string> selectedUsers = new string[] { "", "" };
        protected string value = string.Empty;

        protected async Task CloseModal(string modalId)
        {
            await jSRuntime.InvokeAsync<object>("CloseModal", modalId);
        }
        protected async Task AddSelectedUsers()
        {
            await GroupService.AddMembers(selectedUsers, GroupObject);
            await CloseModal("addMemberModal");
            DataChanged?.Invoke();
        }

        protected override async Task OnInitializedAsync()
        {
            Users = await UserService.Get();
        }

        protected void Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;
            StateHasChanged();
        }
    }
}
